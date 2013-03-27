using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;

namespace BRApplication.Handlers
{
    public class MarketPostHandler
    {
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
                    string userName = AccountHandler.getUserName(profileID);

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

                    marketPost = new MarketPost(title, isBuy, course, condition, userName, datePosted, isbn, author, bookImageURL, price);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the market post --- " + ex.Message);
            }

            return marketPost;
        }

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

                    string postedBy = AccountHandler.getUserName(profileID);

                    Textbook textbook = TextbookHandler.getTextbook(textBookID);
                    string title = textbook.Title;
                    string course = textbook.CourseName;
                    string isbn = textbook.ISBN;
                    string author = textbook.Author;
                    string bookImageURL = textbook.BookImageURL;

                    MarketPost marketPost = new MarketPost(title, isBuy, course, condition, postedBy, datePosted, isbn, author, bookImageURL, price);
                    allMarketPosts.Add(marketPost);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving all market posts --- " + ex.Message);
            }

            return allMarketPosts;
        }
    }
}