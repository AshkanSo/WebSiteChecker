using System.Diagnostics;
using System.Net;
using Microsoft.EntityFrameworkCore;
using SystemLogger.Models;


namespace SystemLogger;

public class Worker 
{
    private readonly ILogger<Worker> _logger;
    private readonly ErorrLogDbContext _context;
    private List<SiteChecker> _siteCheckers;
    private readonly HttpClient _httpClient;
    
    private Dictionary<int, bool> serverStatus = new();
    private Dictionary<int, DateTime?> errorStartTimes = new();
    private Dictionary<int, DateTime?> errorEndTimes = new();

    public Worker(ILogger<Worker> logger,ErorrLogDbContext context,HttpClient httpClient)
    {
        _logger = logger;
        _context = context;
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(15); // تنظیم زمان انتظار
    }
    public async Task ExecuteTask(CancellationToken stoppingToken)
    {
        _siteCheckers = await _context.WebSitesNames.Select(f=>new SiteChecker()
        {
            Id = f.PK_Website,
            Url = f.Url,
            Name = f.Name,
            ServerStatus = true,
            FK_PhoneNumbers = f.FK_PhoneNumbers
        }).ToListAsync(stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var site in _siteCheckers)
            {
                await CheckWebsite(site, stoppingToken);
            }

            await Task.Delay(15000, stoppingToken);
        }
    }

    private async Task CheckWebsite(SiteChecker site, CancellationToken stoppingToken)
    {
        if (!serverStatus.ContainsKey(site.Id))
        {
            serverStatus[site.Id] = true;
            errorStartTimes[site.Id] = null;
            errorEndTimes[site.Id] = null;
        }
        try
        {
            var response = await _httpClient.GetAsync(site.Url, stoppingToken);

            if (response.IsSuccessStatusCode)
            {
                if (!serverStatus[site.Id])
                {
                    var phoneNumbers = await _context.PhoneNumbers
                        .Where(p => p.Websites.Any(w => w.PK_Website == site.Id))
                        .ToListAsync(stoppingToken);

                    foreach (var phoneNumber in phoneNumbers)
                    {
                        SendSms(phoneNumber.PhoneNumber, $"{site.Name} is UP!");
                    }
                    errorEndTimes[site.Id] = DateTime.Now;

                    var errorLog = new ErorrLogs
                    {
                        WebSiteName = site.Name,
                        ErrorCode = "503",
                        StartOfError = errorStartTimes[site.Id].Value,
                        EndOfError = errorEndTimes[site.Id].Value,
                        WebsiteId = site.Id,
                    };

                    _context.ErorrLogs.Add(errorLog);
                    await _context.SaveChangesAsync(stoppingToken);

                    errorStartTimes[site.Id] = null;
                }

                _logger.LogInformation($"{site.Name} : OK");
                serverStatus[site.Id] = true;
            }
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                if (serverStatus[site.Id])
                {
                    errorStartTimes[site.Id] = DateTime.Now;

                    var phoneNumbers = await _context.PhoneNumbers
                        .Where(p => p.Websites.Any(w => w.PK_Website == site.Id))
                        .ToListAsync(stoppingToken);

                    foreach (var phoneNumber in phoneNumbers)
                    {
                        SendSms(phoneNumber.PhoneNumber, $"{site.Name} is DOWN.");
                    }
                }
                serverStatus[site.Id] = false; 
            }
            else
            {
                _logger.LogWarning($"{site.Name} : Unexpected response status : {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
          _logger.LogError($"Failed to check {site.Name} : {e.Message}");
        } 
    }

    private void SendSms(string phoneNumber, string message)
    {
        // Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
        _logger.LogInformation($"Sending SMS to {phoneNumber}: {message}");
    }
}
public class SiteChecker
{
    public string Name { get; set; }
    public string Url { get; set; }
    public int Id { get; set; }
    public bool ServerStatus { get; set; }
    public int FK_PhoneNumbers { get; set; }
}

