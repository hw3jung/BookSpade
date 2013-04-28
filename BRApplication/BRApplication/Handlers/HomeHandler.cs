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
        public static bool SendBugReportEmail(string BugMessage)
        {
            bool success = false;

            try
            {
                string bookSpadeAdminEmail = "asma.patel@hotmail.com";
                EmailUtil e = new EmailUtil(bookSpadeAdminEmail, "", BugMessage);
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
