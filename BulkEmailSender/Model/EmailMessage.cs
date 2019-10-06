using System.Collections.Generic;

namespace BulkEmailSender.Model
{
    public class EmailMessage
    {
        public List<string> ToAddresses { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
