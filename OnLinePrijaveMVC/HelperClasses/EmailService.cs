using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnLinePrijaveMVC.HelperClasses
{
    public interface IEmailService
    {
        void SendEmail(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message,
            params Attachment[] attachments);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
        }

        public void SendEmail(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message,
            params Attachment[] attachments)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(toName, toEmailAddress));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message
            };

            foreach(var attachment in attachments)
            {
                using(var stream = attachment.ContentToStream())
                {
                    body.Attachments.Add(attachment.FileName, stream);
                }
            }

            email.Body = body.ToMessageBody();

            using (var emailClient = new SmtpClient())
            {
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Connect(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
                emailClient.Send(email);
                emailClient.Disconnect(true);
            }
        }
    }
}
