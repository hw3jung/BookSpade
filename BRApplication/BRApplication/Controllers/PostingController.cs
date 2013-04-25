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

        #region Index

        public ActionResult Index(int postID)
        {
            MarketPost marketPost = MarketPostHandler.getMarketPost(postID);

            return View(marketPost);
        }

        #endregion

        #region AddNew

        public ActionResult AddNew()
        {
            IEnumerable<Textbook> textBookCollection = TextbookHandler.getAllTextbooks();
            
            return View(textBookCollection);
        }

        #endregion

        #region GetTextbooks
        [HttpPost]
        public ActionResult GetTextbooks(string searchString = "")
        {
            IEnumerable<Textbook> bookCollection = null;

            if (RegexUtil.isISBN(searchString))
            {
                bookCollection = TextbookHandler.getTextbooksByISBN(searchString);
            }
            else if (RegexUtil.isCourse(searchString))
            {
                bookCollection = TextbookHandler.getTextbooksByCourse(searchString);
            }
            else if (RegexUtil.isTitle(searchString))
            {
                bookCollection = TextbookHandler.getTextbooksByTitle(searchString);
            }
            else if (searchString == String.Empty)
            {
                bookCollection = TextbookHandler.getAllTextbooks();
            }

            if (bookCollection == null)
            {
                bookCollection = new List<Textbook>();
            }

            return PartialView("BookList_Partial", bookCollection);
        }

        #endregion

        #region LoadDetailPost

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
            int bookId = TextbookHandler.insert(newBook);

            string accesstoken = Convert.ToString(Session["AccessToken"]);

            FacebookClient client = new FacebookClient(accesstoken);

            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string FacebookID = Convert.ToString(user["id"]);

            UserProfile profile = AccountHandler.getUserProfile_Facebook(FacebookID);


            if (bookId >= 0)
            {
                return SavePost(profile.ProfileID, newBook.CourseName, newBook.Title, isBuy, price, condition, email);
            }
            else
            {
                return Json("Failed to insert textbook: " + newBook.Title);
            }
        }

        #endregion

        #region SavePost

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

            string accesstoken = Convert.ToString(Session["AccessToken"]); 

            FacebookClient client = new FacebookClient(accesstoken); 

            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string FacebookID = Convert.ToString(user["id"]);  
            
            UserProfile profile =  AccountHandler.getUserProfile_Facebook(FacebookID);

            bool success = false;

            if (email != String.Empty)
            {
                success = AccountHandler.updateUserProfile_Email(FacebookID, email);
            }

            Post newPost = new Post(profile.ProfileID, textbookID, isBuy, price, condition, email != "");
            int postID = PostHandler.insert(newPost);

           
            PostHandler.getPostID(newPost);              
          
            return Json(postID);

        }

        #endregion

    }
}
