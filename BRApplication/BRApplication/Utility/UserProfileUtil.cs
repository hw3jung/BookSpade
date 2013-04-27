using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Handlers;
using BRApplication.Models;
using Facebook; 

namespace BRApplication.Utility
{
    public class UserProfileUtil
    {
        #region getProfileID

        public static int getProfileID(string accesstoken) {

            FacebookClient client = new FacebookClient(accesstoken);
            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string FacebookID = Convert.ToString(user["id"]);
            int profileID = AccountHandler.getProfileID_Facebook(FacebookID);

            return profileID; 
        }

        #endregion

        #region getFacebookID

        public static string getFacebookID(string accesstoken)
        {
            FacebookClient client = new FacebookClient(accesstoken);

            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string FacebookID = Convert.ToString(user["id"]);

            return FacebookID; 
        }

        #endregion

        #region getUserProfile

        public static UserProfile getUserProfile(string accesstoken)
        {
            FacebookClient client = new FacebookClient(accesstoken);
            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string FacebookID = Convert.ToString(user["id"]);
            UserProfile profile = AccountHandler.getUserProfile_Facebook(FacebookID);

            return profile;

        }

        #endregion

    }
}