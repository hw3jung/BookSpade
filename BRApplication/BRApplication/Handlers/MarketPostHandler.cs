using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;
using BRApplication.Utility;

namespace BRApplication.Handlers
{
    public class MarketPostHandler
    {
        #region Market Posts by Textbook ID
        private static IEnumerable<MarketPost> getMarketPostsByTextbookIDs(int[] textbookIDs)
        {
            List<MarketPost> marketPosts = new List<MarketPost>();
            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                foreach (int textbookID in textbookIDs)
                {
                    DataTable dt = DAL.select(String.Format("TextBookID = '{0}'", textbookID), "Posts");

                    foreach (DataRow row in dt.Rows)
                    {
                        bool isBuy = Convert.ToBoolean(row["IsBuy"]);
                        string condition = Convert.ToString(row["BookCondition"]);
                        int profileID = Convert.ToInt32(row["ProfileID"]);
                        DateTime datePosted = Convert.ToDateTime(row["CreatedDate"]);
                        int price = Convert.ToInt32(row["Price"]);
                        int postID = Convert.ToInt32(row["PostID"]); 

                        UserProfile UserProfile = AccountHandler.getUserProfile(profileID);
                        string postedBy = UserProfile.Name;
                        string email = UserProfile.Email; 

                        Textbook textbook = TextbookHandler.getTextbook(textbookID);
                        string title = textbook.Title;
                        string course = textbook.CourseName;
                        string isbn = textbook.ISBN;
                        string author = textbook.Author;
                        string bookImageURL = textbook.BookImageURL;
                        List<Bid> bids = new List<Bid>();

                        MarketPost marketPost = new MarketPost(title, isBuy, course, condition, postedBy, datePosted, isbn, author, bookImageURL, price, bids);
                        marketPost.profileID = profileID;
                        marketPost.PostID = postID;
                        marketPost.email = email; 
                        marketPosts.Add(marketPost);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving market posts --- " + ex.Message);
            }

            return marketPosts;
        }
        #endregion

        #region Market Posts by ISBN
        public static IEnumerable<MarketPost> getMarketPostsByISBN(string isbn)
        {
            IEnumerable<MarketPost> marketPosts = null;

            try
            {
                int[] textbookIDs = TextbookHandler.getTextbookIDsByISBN(isbn);
                marketPosts = getMarketPostsByTextbookIDs(textbookIDs);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving market posts by ISBN --- " + ex.Message);
            }

            return marketPosts;
        }
        #endregion

        #region Market Posts by Course
        public static IEnumerable<MarketPost> getMarketPostsByCourse(string courseName)
        {
            IEnumerable<MarketPost> marketPosts = null;

            try
            {
                int[] textbookIDs = TextbookHandler.getTextbookIDsByCourse(courseName);
                marketPosts = getMarketPostsByTextbookIDs(textbookIDs);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving market posts by course --- " + ex.Message);
            }

            return marketPosts;
        }
        #endregion

        #region Market Posts by Title
        public static IEnumerable<MarketPost> getMarketPostsByTitle(string title)
        {
            IEnumerable<MarketPost> marketPosts = null;

            try
            {
                int[] textbookIDs = TextbookHandler.getTextbookIDsByTitle(title);
                marketPosts = getMarketPostsByTextbookIDs(textbookIDs);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving market posts by book title --- " + ex.Message);
            }

            return marketPosts;
        }
        #endregion

        #region All Market Posts
        public static IEnumerable<MarketPost> getAllMarketPosts()
        {
            List<MarketPost> allMarketPosts = new List<MarketPost>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select("", "Posts");

                foreach (DataRow row in dt.Rows)
                {
                    int textBookID = Convert.ToInt32(row["TextBookID"]);
                    bool isBuy = Convert.ToBoolean(row["IsBuy"]);
                    string condition = Convert.ToString(row["BookCondition"]);
                    int profileID = Convert.ToInt32(row["ProfileID"]);
                    DateTime datePosted = Convert.ToDateTime(row["CreatedDate"]);
                    int price = Convert.ToInt32(row["Price"]);
                    int postID = Convert.ToInt32(row["PostID"]); 

                    UserProfile UserProfile = AccountHandler.getUserProfile(profileID);
                    string postedBy = UserProfile.Name;
                    string email = UserProfile.Email; 

                    Textbook textbook = TextbookHandler.getTextbook(textBookID);
                    string title = textbook.Title;
                    string course = textbook.CourseName;
                    string isbn = textbook.ISBN;
                    string author = textbook.Author;
                    string bookImageURL = textbook.BookImageURL;
                    List<Bid> bids = new List<Bid>();

                    MarketPost marketPost = new MarketPost(title, isBuy, course, condition, postedBy, datePosted, isbn, author, bookImageURL, price, bids);
                    marketPost.profileID = profileID;
                    marketPost.email = email;
                    marketPost.PostID = postID; 

                    allMarketPosts.Add(marketPost);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving all market posts --- " + ex.Message);
            }

            return allMarketPosts;
        }
        #endregion

        #region Market Post by Post ID 
        public static MarketPost getMarketPost(int postID)
        {
            MarketPost marketPost = null;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("PostID = '{0}'", postID), "Posts");
                

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    int profileID = Convert.ToInt32(row["ProfileID"]);
                    UserProfile UserProfile = AccountHandler.getUserProfile(profileID);
                    string postedBy = UserProfile.Name;
                    string email = UserProfile.Email; 

                    int textbookID = Convert.ToInt32(row["TextBookID"]);
         
                    Textbook textbook = TextbookHandler.getTextbook(textbookID);
                    string isbn = textbook.ISBN;
                    string title = textbook.Title;
                    string author = textbook.Author;
                    string course = textbook.CourseName;
                    string bookImageURL = textbook.BookImageURL;

                    bool isBuy = Convert.ToBoolean(row["IsBuy"]);
                    string condition = Convert.ToString(row["BookCondition"]);
                    int price = Convert.ToInt32(row["Price"]);
                    DateTime datePosted = Convert.ToDateTime(row["CreatedDate"]);
                    IEnumerable<Bid> bids = BidHandler.getBids(postID);

                    marketPost = new MarketPost(title, isBuy, course, condition, postedBy, datePosted, isbn, author, bookImageURL, price, bids);
                    marketPost.profileID = profileID;
                    marketPost.PostID = postID; 
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the market post --- " + ex.Message);
            }

            return marketPost;
        }
        #endregion

        #region Send Seller Email 
        //(Respondant, PostedBy, textbook, RespondantEmail, PosterEmail); 
        public static bool SendSellerMail(string Respondant, string PostedBy, string textbook, string RespondantEmail, string PosterEmail)
        {
            bool success = false;

            try
            {
                string subject = String.Format("{0} : {1} wants to buy this textbook", textbook, Respondant);
                string body = string.Empty;
                body += String.Format("Hello {0}, <br/><br/>", PostedBy, Environment.NewLine);
                body += String.Format("Congratulations! <br/><br/> {0} wants to buy {1} textbook from you.", Respondant, textbook);
                body += String.Format("Please contact <b>{0}</b> through this email address: <br/><br/><b>{1}</b><br/><br/>", Respondant, RespondantEmail);
                body += "Thank You, <br/>";
                body += "BookSpade Team";

                EmailUtil e = new EmailUtil(PosterEmail, subject, body);
                success = true;
            }
            catch (Exception e)
            {
                Console.Write("ERROR: failed to send email ---- " + e.Message); 
            }

            return success; 
        }
        #endregion

        #region Send Buyer Email
        public static bool SendBuyerMail(string Respondant, string PostedBy, string textbook, string RespondantEmail, string PosterEmail)
        {
            bool success = false;

            try
            {
                string subject = String.Format("{0} : {1} wants to buy this textbook", textbook, Respondant);
                string body = string.Empty;
                body += String.Format("Hello {0}, <br/><br/>", PostedBy, Environment.NewLine);
                body += String.Format("Congratulations! <br/><br/> {0} wants to sell {1} to you.", Respondant, textbook);
                body += String.Format("Please contact <b>{0}</b> through this email address: <br/><br/><b>{1}</b><br/><br/>", Respondant, RespondantEmail);
                body += "Thank You, <br/>";
                body += "BookSpade Team";

                EmailUtil e = new EmailUtil(PosterEmail, subject, body);
                success = true;
            }
            catch (Exception e)
            {
                Console.Write("ERROR: failed to send email ---- " + e.Message);
            }

            return success;

        }
        #endregion

        #region Market Posts by IsBuy

        public static IEnumerable<MarketPost> getMarketPostsByisBuy(bool isBuy)
        {
            List<MarketPost> marketPosts = new List<MarketPost>();
            DataAccessLayer DAL = new DataAccessLayer(); 

            try
            {
                DataTable dt = DAL.select(String.Format("IsBuy = '{0}'", isBuy), "Posts");

                foreach (DataRow row in dt.Rows)
                {
                    bool IsBuy = Convert.ToBoolean(row["IsBuy"]);
                    string condition = Convert.ToString(row["BookCondition"]);
                    int profileID = Convert.ToInt32(row["ProfileID"]);
                    DateTime datePosted = Convert.ToDateTime(row["CreatedDate"]);
                    int price = Convert.ToInt32(row["Price"]);
                    int postID = Convert.ToInt32(row["PostID"]);
                    int textBookID = Convert.ToInt32(row["TextBookID"]);

                    UserProfile UserProfile = AccountHandler.getUserProfile(profileID);
                    string postedBy = UserProfile.Name;
                    string email = UserProfile.Email; 

                    Textbook textbook = TextbookHandler.getTextbook(textBookID);
                    string title = textbook.Title;
                    string course = textbook.CourseName;
                    string isbn = textbook.ISBN;
                    string author = textbook.Author;
                    string bookImageURL = textbook.BookImageURL;
                    List<Bid> bids = new List<Bid>();

                    MarketPost marketPost = new MarketPost(title, isBuy, course, condition, postedBy, datePosted, isbn, author, bookImageURL, price, bids);
                    marketPost.profileID = profileID;
                    marketPost.PostID = postID;
                    marketPost.email = email; 
                    marketPosts.Add(marketPost);
                }
               
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving market posts --- " + ex.Message);
            }

            return marketPosts;
        }

        #endregion

    }
}