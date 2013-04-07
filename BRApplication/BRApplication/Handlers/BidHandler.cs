using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;

namespace BRApplication.Handlers
{
    public class BidHandler
    {
        public static IEnumerable<Bid> getBids(int postID)
        {
            List<Bid> bids = new List<Bid>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("postID = '{0}'", postID), "Bids");

                foreach (DataRow row in dt.Rows)
                {
                    int buyerID = Convert.ToInt32(row["BuyerID"]);
                    int sellerID = Convert.ToInt32(row["SellerID"]);

                    DataTable buyerDt = DAL.select(String.Format("profileID = '{0}'", buyerID), "UserProfile");
                    DataRow buyerRow = buyerDt.Rows[0];
                    string buyer = Convert.ToString(buyerRow["Name"]);

                    DataTable sellerDt = DAL.select(String.Format("profileID = '{0}'", sellerID), "UserProfile");
                    DataRow sellerRow = sellerDt.Rows[0];
                    string seller = Convert.ToString(sellerRow["Name"]);

                    decimal bidPrice = Convert.ToDecimal(row["BidPrice"]);

                    Bid bid = new Bid(postID, buyerID, sellerID, buyer, seller, bidPrice);
                    bids.Add(bid);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the bids --- " + ex.Message);
            }

            return bids;
        }
    }
}