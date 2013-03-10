using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class UserProfileModel
    {
        // Primary Key ProfileID
        public int ProfileID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        internal bool insert()
        {
            throw new NotImplementedException();
        }
    }
}