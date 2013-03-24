using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Models;
using BRApplication.Handlers; 

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
            // Uncomment when db is ready
            //IEnumerable<Textbook> textBookCollection = TextbookHandler.getAllTextbooks();

            //// Testdata
            List<Textbook> testData = new List<Textbook>();

            int numPosts = 15;
            for (int i = 0; i < numPosts; i++)
            {
                string isbn = "10 0-470-56479-" + i;
                string title = "Managerial Accounting - Ninth Canadian Edition" + i;
                string author = "Author " + i;
                string courseName = "AFM 23" + i;
                Textbook textBook = new Textbook(isbn, title, author, courseName, "", 100); 
                testData.Add(textBook);
            }
            //////////////////////////////////
            
            // Uncomment when db is ready
            //return View(textBookCollection);

            return View(testData);
        }

        [HttpPost]
        public ActionResult SaveBook(string isbn,
                                     string title,
                                     string author,
                                     string course,
                                     string bookImageURL,
                                     bool isBuy,
                                     int price,
                                     string condition)
        {
            int courseID = CourseInfo.getCourseID(course);
            if (courseID == -1)
            {
                CourseInfo newCourse = new CourseInfo(course);
                CourseInfoHandler.insert(newCourse);
            }

            Textbook newBook = new Textbook(isbn, title, author, course, bookImageURL, price);
            bool success = TextbookHandler.insert(newBook);

            if (success)
            {
                // TODO: implement retrieval of user profile ID and include it as part of a new Post
                return SavePost(0, newBook.CourseName, newBook.Title, isBuy, price, condition);
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
                                     string condition)
        {
            int textbookID = TextbookHandler.getTextbookID(course, title);

            Post newPost = new Post(profileID, textbookID, isBuy, price, condition);
            bool success = PostHandler.insert(newPost);

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
