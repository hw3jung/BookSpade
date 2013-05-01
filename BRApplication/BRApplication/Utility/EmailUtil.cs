using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using BRApplication.Models;

namespace BRApplication.Utility
{
    public class EmailUtil
    {

        public EmailUtil(string email, string subject, string body)
        {
            Mail(email, subject, body);
        }

        public static void Mail(string email, string subject, string body)
        {
            WebMail.EnableSsl = true; 
            WebMail.SmtpServer = "smtpout.secureserver.net";
            WebMail.SmtpPort = 465;
            WebMail.UserName = "info@bookspade.com";
            WebMail.Password = "SpadeIt";
            WebMail.Send(
                email,
                subject,
                body,
                from: "info@bookspade.com"
            );
        }
    }
}