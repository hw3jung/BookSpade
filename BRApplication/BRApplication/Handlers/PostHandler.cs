using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;

namespace BRApplication.Handlers
{
    public class PostHandler
    {
        public static bool insert(Post newPost)
        {
            bool success = false;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("ProfileID = '{0}' AND TextBookID = '{1}' AND IsBuy = '{2}'", newPost.ProfileID, newPost.TextBookID, newPost.IsBuy), "Posts");

                if (dt == null || dt.Rows.Count == 0)
                {
                    Dictionary<string, string> post = new Dictionary<string, string>();
                    post.Add("ProfileID", Convert.ToString(newPost.ProfileID));
                    post.Add("TextBookID", Convert.ToString(newPost.TextBookID));
                    post.Add("IsBuy", Convert.ToString(newPost.IsBuy));
                    post.Add("Price", Convert.ToString(newPost.Price));
                    post.Add("BookCondition", newPost.Condition);
                    post.Add("ExpiryDate", Convert.ToString(newPost.ExpiryDate));
                    post.Add("IsActive", "1");
                    post.Add("IsDeleted", "0");
                    post.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    post.Add("ModifiedDate", Convert.ToString(DateTime.Now));
                    success = DAL.insert(post, "Posts");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new post --- " + ex.Message);
            }

            return success;
        }

        public static int getPostID(Post newPost)
        {
            int postID = -1;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("ProfileID = '{0}' AND TextBookID = '{1}' AND IsBuy = '{2}'", newPost.ProfileID, newPost.TextBookID, newPost.IsBuy), "Posts");

                if (dt != null && dt.Rows.Count > 0)
                {
                    postID = Convert.ToInt32(dt.Rows[0]["PostID"]);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the post ID --- " + ex.Message);
            }

            return postID;
        }
    }
}