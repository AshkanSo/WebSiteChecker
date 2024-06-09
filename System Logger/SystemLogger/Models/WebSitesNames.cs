using Dapper;

namespace SystemLogger.Models;

public class Website
{
    public int PK_Website { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public bool ServerStatus { get; set; } = true;
    public int FK_PhoneNumbers { get; set; }

    public ICollection<ErorrLogs> ErorrLogs { get; set; }
    public ICollection<PhoneNumbers> PhoneNumbers { get; set; }
}