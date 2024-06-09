namespace SystemLogger.Models;

public class PhoneNumbers
{
    public int PK_PhoneNumber { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Website> Websites { get; set; }
    public ICollection<ErorrLogs> ErorrLogs { get; set; }
    public PhoneNumbers()
    {
        ErorrLogs = new List<ErorrLogs>();
    }
}