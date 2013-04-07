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
            WebMail.SmtpServer = "smtp.live.com";
            WebMail.SmtpPort = 587;
            WebMail.UserName = "asma.patel@hotmail.com";
            WebMail.Password = "Bubbletea1207";
            WebMail.Send(
                email,
                subject,
                body,
                from: "asma.patel@hotmail.com"
            );
        }
    }
}