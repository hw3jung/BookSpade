using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Models;
using BRApplication.Handlers;
using BRApplication.Utility; 
using Microsoft.Web.WebPages.OAuth;
using Facebook; 


namespace BRApplication.Controllers
{
    public class PostingController : Controller
    {
        //
        // GET: /Posting/

        public ActionResult Index(int postID)
        {
            MarketPost marketPost = MarketPostHandler.getMarketPost(postID);

            return View(marketPost);
        }

        public ActionResult AddNew()
        {
            IEnumerable<Textbook> textBookCollection = TextbookHandler.getAllTextbooks();
            
            return View(textBookCollection);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LoadDetailPost()
        {
            MarketPost marketPost = (MarketPost)Session["marketPost"];
            return View("Index", marketPost); 
        }


        [HttpPost]
        
        public ActionResult SaveBook(string isbn, 
                                     string title,
                                     string author,
                                     string course,
                                     string bookImageURL,
                                     bool isBuy,
                                     int price,
                                     string condition,
                                     string email)
        {
            int courseID = CourseInfoHandler.getCourseID(course);
            if (courseID == -1)
            {
                // TODO: Implement description for each course
                CourseInfo newCourse = new CourseInfo(course, String.Empty);
                CourseInfoHandler.insert(newCourse);
            }

            Textbook newBook = new Textbook(isbn, title, author, course, bookImageURL, price);
            bool success = TextbookHandler.insert(newBook);

            if (success)
            {
                // TODO: implement retrieval of user profile ID and include it as part of a new Post
                return SavePost(0, newBook.CourseName, newBook.Title, isBuy, price, condition, email);
            }
            else
            {
                return Json("Failed to insert textbook: " + newBook.Title);
            }
        }

        [HttpPost]
        public ActionResult SavePost(int profileID, 
                                     string course, 
                                     string title, 
                                     bool isBuy, 
                                     int price, 
                                     string condition,
                                     string email)
        {
            int textbookID = TextbookHandler.getTextbookID(course, title);

            //Retrieve current user's facebook ID using the facebooktoken

            string accesstoken = Convert.ToString(Session["facebooktoken"]); 

            FacebookClient client = new FacebookClient(accesstoken); 

            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string FacebookID = Convert.ToString(user["id"]);  
            
            UserProfile profile =  AccountHandler.getUserProfile_Facebook(FacebookID);

            bool success = false; 

            if (email != "")
                success = AccountHandler.updateUserProfile_Email(FacebookID, email);

            Post newPost = new Post(profile.ProfileID, textbookID, isBuy, price, condition, email != "");
            success = PostHandler.insert(newPost);

            if (success)
            {
                int postID = PostHandler.getPostID(newPost);
                return Index(postID);
            }
            else
            {
                return Json("Failed to insert post for: " + title);
            }
        }

        

    }
}
