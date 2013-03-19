using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Models; 

namespace BRApplication.Controllers
{
    public class PostingController : Controller
    {
        //
        // GET: /Posting/

        public ActionResult Index()
        {
            string Title = "Managerial Accounting - Ninth Canadian Edition";
            string Course = "AFM 102";
            string Condition = "Good";
            string PostedBy = "John Smith";
            DateTime Date = new DateTime(2013, 2, 21, 12, 0, 0);
            string ISBN = "10 0-470-56479-2";
            double price = 50.00;
            string Author = "Walter T. Harrison";
            MarketPostModel mpm = new MarketPostModel(Title, false, Course, Condition, PostedBy, Date, ISBN, Author, price); 
            return View(mpm);
        }

        public ActionResult AddNew()
        {
            /////////////
            // Test Data
            /////////////

            List<Textbook> testData = new List<Textbook>();
            int numPosts = 15;
            for (int i = 0; i < numPosts; i++)
            {
                int textBookID = i;
                string isbn = "10 0-470-56479-" + i;
                string title = "Managerial Accounting - Ninth Canadian Edition" + i;
                string author = "Author " + i;
                string edition = "Edition " + i;
                int courseID = i;
                DateTime date = new DateTime(2013, 2, 21, 12, 0, 0);
                Textbook textBook = new Textbook(textBookID, isbn, title, author, edition, courseID, true, false, date, date);
                testData.Add(textBook);
            }

            return View(testData);
        }
    }
}
