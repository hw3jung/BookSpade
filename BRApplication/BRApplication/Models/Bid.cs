using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class Bid
    {
        // Primary Key BidID
        public int BidID { get; set; }

        // Foreign Key PostID
        public int PostID { get; set; }

        // Foreign Key Buyer's Profile ID
        public int BuyerID { get; set; }

        // Foreign Key Seller's Profile ID
        public int SellerID { get; set; }

        public string Buyer { get; set; }
        public string Seller { get; set; }

        public decimal BidPrice { get; set; }

        public Bid (int postID, 
                    int buyerID, 
                    int sellerID,
                    string buyer,
                    string seller,
                    decimal bidPrice)
        {
            this.PostID = postID;
            this.BuyerID = buyerID;
            this.SellerID = sellerID;
            this.Buyer = buyer;
            this.Seller = seller;
            this.BidPrice = bidPrice;
        }
    }
}