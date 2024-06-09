using Microsoft.EntityFrameworkCore;
using SystemLogger;
using SystemLogger.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddDbContext<ErorrLogDbContext>(options =>
    options.UseSqlite("Data Source=errors.db"));

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Logging.AddEventLog(); // اضافه کردن EventLog

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));


builder.Services.AddHttpClient<Worker>(); // اضافه کردن HttpClient

builder.Services.AddScoped<Worker>();
builder.Services.AddHostedService<ScopedWorker>();

var host = builder.Build();

host.Run();
