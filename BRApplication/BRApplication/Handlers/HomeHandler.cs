using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;
using BRApplication.Utility;

namespace BRApplication.Handlers
{
    public class HomeHandler
    {
        public static bool SendBugReportEmail(string BugMessage, int bugNumber)
        {
            bool success = false;

            try
            {
                string bookSpadeAdminEmail = "info@bookspade.com";
                EmailUtil e = new EmailUtil(bookSpadeAdminEmail, "User Reported Bug: #" + bugNumber, BugMessage);
                success = true;
            }
            catch (Exception e)
            {
                Console.Write("ERROR: failed to send email ---- " + e.Message);
            }

            return success;
        }
    }
}
