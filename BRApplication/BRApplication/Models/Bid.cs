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

        public int BidderID { get; set; }
        
        public string Bidder { get; set; }

        public int BidPrice { get; set; }

        public bool BidviaEmail { get; set; }

        public Bid (int postID, 
                    int bidderID,
                    string bidder,
                    int bidPrice,
                    bool bidviaEmail,
                    int BidID
            )
        {
            this.PostID = postID;
            this.BidderID = bidderID;
            this.Bidder = bidder;
            this.BidPrice = bidPrice;
            this.BidviaEmail = bidviaEmail;
            this.BidID = BidID; 
        }
    }
}