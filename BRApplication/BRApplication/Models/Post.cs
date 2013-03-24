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

        public bool IsBuy { get; set; }
        public int Price { get; set; }
        public string Condition { get; set; }
        public DateTime ExpiryDate { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Post(int profileID, 
                    int textbookID, 
                    bool isBuy, 
                    int price,
                    string condition)
        {
            this.ProfileID = profileID;
            this.TextBookID = textbookID;
            this.IsBuy = isBuy;
            this.Price = price;
            this.Condition = condition;

            DateTime now = DateTime.Now;
            this.ExpiryDate = now.AddMonths(8);

            this.IsActive = true;
            this.IsDeleted = false;
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }
    }
}