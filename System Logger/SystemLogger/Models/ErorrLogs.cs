using System.Runtime.InteropServices.JavaScript;

namespace SystemLogger.Models
{
    public class ErorrLogs
    {
        public int PK_ErrorLog { get; set; }
        public string WebSiteName { get; set; }
        public string ErrorCode { get; set; }
        public DateTime StartOfError { get; set; }
        public DateTime EndOfError { get; set; }
        public List<PhoneNumbers> PhoneNumbers { get; set; }

        public int WebsiteId { get; set; }
        public Website Website { get; set; }

        
        public ErorrLogs()
        {
            PhoneNumbers = new List<PhoneNumbers>();
        }
        public ErorrLogs(string webSiteName, string errorCode, DateTime startOfError, DateTime endOfError, List<PhoneNumbers> phoneNumbers, int websiteId)
        {
            WebSiteName = webSiteName;
            ErrorCode = errorCode;
            StartOfError = startOfError;
            EndOfError = endOfError;
            PhoneNumbers = phoneNumbers;
            WebsiteId = websiteId;
        }

    }
}