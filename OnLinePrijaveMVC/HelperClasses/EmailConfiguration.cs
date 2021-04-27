using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace OnLinePrijaveMVC.HelperClasses
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
    }

    public class EmailConfiguration : IEmailConfiguration
    {
        public EmailConfiguration()
        {
            SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            SmtpPort = Convert.ToInt16(ConfigurationManager.AppSettings["Port"]);
        }

        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }
}
