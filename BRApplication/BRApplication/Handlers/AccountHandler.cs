using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;


namespace BRApplication.Handlers
{
    public class AccountHandler
    {
        public bool AddUser(ExternalLoginModel elm)
        {
            bool success = false;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("FacebookID = '{0}'", elm.FacebookID), "UserProfile");
                
                if (dt == null ||dt.Rows.Count == 0)
                {
                    Dictionary<string, string> Profile = new Dictionary<string, string>();
                    Profile.Add("FacebookID", elm.FacebookID);
                    Profile.Add("Name", elm.Name);
                    Profile.Add("Email", elm.Email);
                    Profile.Add("FacebookProfileLink", elm.FacebookProfileLink);
                    Profile.Add("IsActive", "1");
                    Profile.Add("IsDeleted", "0");
                    Profile.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    Profile.Add("ModifiedDate", Convert.ToString(DateTime.Now));
                    success = DAL.insert(Profile, "UserProfile");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new user --- " + ex.Message); 
            }

            return success; 
        }
    }
}