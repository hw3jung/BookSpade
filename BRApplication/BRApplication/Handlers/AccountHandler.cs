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
        public static bool AddUser(UserProfile elm)
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

        public static UserProfile getUserProfile(int profileID)
        {
            UserProfile profile = null;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("ProfileID = '{0}'", profileID), "UserProfile");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string facebookID = Convert.ToString(row["FacebookID"]);
                    string name = Convert.ToString(row["Name"]);
                    string email = Convert.ToString(row["Email"]);
                    string facebookProfileLink = Convert.ToString(row["FacebookProfileLink"]);
                    string gender = Convert.ToString(row["Gender"]);

                    profile = new UserProfile(name, facebookProfileLink, facebookID, gender, email, String.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;
        }

        public static UserProfile getUserProfile_Facebook(string facebookID)
        {
            UserProfile profile = null;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("FacebookID = '{0}'", facebookID), "UserProfile");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string FacebookID = Convert.ToString(row["FacebookID"]);
                    string name = Convert.ToString(row["Name"]);
                    string email = Convert.ToString(row["Email"]);
                    string facebookProfileLink = Convert.ToString(row["FacebookProfileLink"]);
                    string gender = Convert.ToString(row["Gender"]);
                    string profileID = Convert.ToString(row["ProfileID"]); 

                    profile = new UserProfile(name, facebookProfileLink, FacebookID, gender, email, String.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;
        }


        public static bool updateUserProfile_Email(string facebookID, string Email)
        {
            bool success = false;

            try
            {
                Dictionary<string, string> Profile = new Dictionary<string,string>();
                Profile.Add("Email", Email); 

                DataAccessLayer DAL = new DataAccessLayer();
                DAL.update("UserProfile", String.Format("FacebookID = '{0}'", facebookID), Profile);

            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in updating user profile with ethe given email --- " + ex.Message);
            }

            return success;
        }


        public static string getUserName(int profileID)
        {
            UserProfile profile = getUserProfile(profileID);

            return profile.Name;
        }
    }
}