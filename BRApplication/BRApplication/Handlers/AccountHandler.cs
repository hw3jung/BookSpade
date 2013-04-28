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

        #region Add User 

        public static int AddUser(UserProfile elm)
        {
            int id = -1;

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
                    Profile.Add("Gender", elm.Gender);
                    Profile.Add("IsActive", "1");
                    Profile.Add("IsDeleted", "0");
                    Profile.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    Profile.Add("ModifiedDate", Convert.ToString(DateTime.Now));
                    id = DAL.insert(Profile, "UserProfile");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new user --- " + ex.Message); 
            }

            return id; 
        }

    #endregion

        #region getUserProfile

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

        #endregion

        #region getUserByFacebook

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
                    int profileID = Convert.ToInt32(row["ProfileID"]); 

                    profile = new UserProfile(name, facebookProfileLink, FacebookID, gender, email, String.Empty);
                    profile.ProfileID = profileID; 
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profile;
        }

        #endregion

        #region getProfileIDByFacebook

        public static int getProfileID_Facebook(string facebookID)
        {
            int profileID = -1; 

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("FacebookID = '{0}'", facebookID), "UserProfile");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    profileID = Convert.ToInt32(row["ProfileID"]); 
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the user profile --- " + ex.Message);
            }

            return profileID;
        }

        #endregion

        #region updateUserProfile_Email

        public static bool updateUserProfile_Email(int ProfileID, string Email)
        {
            bool success = false;

            try
            {
                Dictionary<string, string> Profile = new Dictionary<string,string>();
                Profile.Add("Email", String.Format("'{0}'", Email)); 

                DataAccessLayer DAL = new DataAccessLayer();
                DAL.update("UserProfile", String.Format("ProfileID = '{0}'", ProfileID), Profile);

            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in updating user profile with ethe given email --- " + ex.Message);
            }

            return success;
        }

        #endregion

        #region getUserName
        
        public static string getUserName(int profileID)
        {
            UserProfile profile = getUserProfile(profileID);

            return profile.Name;
        }

        #endregion
    }
}