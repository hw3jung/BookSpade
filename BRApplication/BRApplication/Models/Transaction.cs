using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class Transaction
    {
        // Primary Key TransactionID
        public int TransActionID { get; set; }

        public int PostID { get; set; }
        public int BuyerID { get; set; }

        // Foreign Key SellerID
        public int SellerID { get; set; }
        public int Status { get; set; }

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