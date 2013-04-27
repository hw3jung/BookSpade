using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;
using BRApplication.Utility; 

namespace BRApplication.Handlers
{
    public class BidHandler
    {
        #region getBids

        public static List<Bid> getBids(int postID)
        {
            List<Bid> bids = new List<Bid>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("postID = '{0}'", postID), "Bids");

                foreach (DataRow row in dt.Rows)
                {
                    int bidderID = Convert.ToInt32(row["BidderID"]);

                    DataTable buyerDt = DAL.select(String.Format("profileID = '{0}'", bidderID), "UserProfile");
                    DataRow buyerRow = buyerDt.Rows[0];
                    
                    string bidder = Convert.ToString(buyerRow["Name"]);
                    decimal bidPrice = Convert.ToDecimal(row["BidPrice"]);
                    bool viaEmail = Convert.ToBoolean(row["viaEmail"]); 

                    Bid bid = new Bid(postID, bidderID, bidder, bidPrice, viaEmail);
                    bids.Add(bid);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the bids --- " + ex.Message);
            }

            return bids;
        }

        #endregion

        #region createBid

        public static int createBid(Bid bid) 
        {
            int bidID = -1;
            DataAccessLayer DAL = new DataAccessLayer();
            Dictionary<string, string> Bid = new Dictionary<string, string>();

            Bid.Add("BidPrice", Convert.ToString(bid.BidPrice));
            Bid.Add("PostID", Convert.ToString(bid.PostID));
            Bid.Add("BidderID", Convert.ToString(bid.BidderID));
            Bid.Add("viaEmail", Convert.ToString(bid.BidviaEmail)); 
            Bid.Add("IsActive", Convert.ToString(true));
            Bid.Add("IsDeleted", Convert.ToString(false));
            Bid.Add("CreatedDate", Convert.ToString(DateTime.Now));
            Bid.Add("ModifiedDate", Convert.ToString(DateTime.Now));

            bidID =  DAL.insert(Bid, "Bids");

            return bidID; 
        }

        #endregion

    }
}