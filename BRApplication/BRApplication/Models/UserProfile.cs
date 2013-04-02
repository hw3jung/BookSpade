using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class UserProfile
    {
        public int ProfileID { get; set; }
        public string Name { get; set; }
        public string FacebookProfileLink { get; set; }
        public string FacebookID { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ExternalLoginData { get; set; }
        public bool Verified { get; set; }

        public UserProfile(string name, string facebookProfileLink, string facebookID, string gender, string email, string externalLoginData)
        {
            this.Name = name;
            this.FacebookProfileLink = facebookProfileLink;
            this.FacebookID = facebookID;
            this.Gender = gender;
            this.Email = email;
            this.ExternalLoginData = externalLoginData; 
        }
    }
}