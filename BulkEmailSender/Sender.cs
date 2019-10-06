using BulkEmailSender.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace BulkEmailSender
{
    public class Sender
    {
        private EmailConfiguration _emailConfiguration;
        private IConfigurationRoot _config;
        public Sender()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            _config = builder.Build();

            _emailConfiguration = GetConfiguration();
        }
        public void InitSend()
        {
            EmailMessage message = new EmailMessage()
            {
                Content = _emailConfiguration.Content,
                FromAddress = new EmailAddress() { Name = _emailConfiguration.Name, Address = _emailConfiguration.Email },
                Subject = _emailConfiguration.Subject,
                ToAddresses = GetCSVData()
            };
            SendDefault(message);
        }

        public List<string> GetCSVData()
        {
            List<string> emails = new List<string>();
            using (var reader = new StreamReader(_config["Source"]))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    emails.AddRange(line.Split(','));
                }
            }
            return emails;
        }


        public void SendDefault(EmailMessage emailMessage)
        {
            Logger.Write($"-- Email Service Starts --", "", LogType.Information);
            foreach (var recipient in emailMessage.ToAddresses)
            {
                try
                {
                    SmtpClient SmtpServer = new SmtpClient(_emailConfiguration.SmtpServer);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(emailMessage.FromAddress.Address, emailMessage.FromAddress.Name);
                    mail.To.Add(recipient);
                    mail.Subject = emailMessage.Subject;
                    mail.IsBodyHtml = false;
                    mail.Body = emailMessage.Content;
                    mail.Attachments.Add(new Attachment(_config["Attachment"]));
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                    SmtpServer.EnableSsl = true;
                    Logger.Write($"Sending to: {recipient}", "", LogType.Information);
                    SmtpServer.Send(mail);
                    Logger.Write($"Sending successfull to: {recipient}", "", LogType.Information);
                    Thread.Sleep(40000);
                }
                catch (System.Exception ex)
                {
                    Logger.Write($"Sending failed to: {recipient}", ex.Message, LogType.Error);
                    continue;
                }
            }
            Logger.Write($"-- Email Service Ends --", "", LogType.Information);
        }


        public EmailConfiguration GetConfiguration()
        {
            return _config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        }
    }
}
