using System;
using System.Collections.Generic;
using System.Text;

namespace BulkEmailSender.Model
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
    }

    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
