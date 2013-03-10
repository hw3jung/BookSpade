using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class Post
    {
        // Primary Key PostID
        public int PostID { get; set; }

        // Foreign Key ProfileID
        public int ProfileID { get; set; }

        // Foreign Key TextBookID
        public int TextBookID { get; set; }

        public bool PostType { get; set; }
        public float Price { get; set; }
        public DateTime ExpiryDate { get; set; }
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