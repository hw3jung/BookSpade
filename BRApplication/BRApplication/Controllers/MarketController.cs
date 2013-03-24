using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Models;
using BRApplication.Handlers;
using System.Data;
using System.Diagnostics;

namespace BRApplication.Controllers
{
    public class MarketController : Controller
    {
        //
        // GET: /Market/

        public string getCourseName(int textBookID, DataAccessLayer d)
        {
            DataTable dt = new DataTable();
            dt = d.getCourseNameWithTextBookID(textBookID);
            string ret = null;
            DataRow row = dt.Rows[0];
            ret = (string)row[0];
            return ret;
        }

        public string getTextBookTitle(int textBookID, DataAccessLayer d)
        {
            DataTable dt = new DataTable();
            dt = d.select("TextBookID = " + textBookID, "TextBooks");
            string ret = null;
            DataRow row = dt.Rows[0];
            ret = (string)row[2];
            return ret;
        }

        public string getPostedBy(int profileID, DataAccessLayer d)
        {
            DataTable dt = new DataTable();
            dt = d.select("ProfileID = " + profileID, "UserProfile");
            string ret = null;
            DataRow row = dt.Rows[0];
            ret = (string)row[1];
            return ret;
        }

        public string getISBN(int textBookID, DataAccessLayer d)
        {
            DataTable dt = new DataTable();
            dt = d.select("textBookID = " + textBookID, "TextBooks");
            string ret = null;
            DataRow row = dt.Rows[0];
            ret = (string)row[1];
            return ret;
        }

        public string getAuthor(int textBookID, DataAccessLayer d)
        {
            DataTable dt = new DataTable();
            dt = d.select("textBookID = " + textBookID, "TextBooks");
            string ret = null;
            DataRow row = dt.Rows[0];
            ret = (string)row[3];
            return ret;
        }

        public ActionResult Index()
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<MarketPost> mpmLst = new List<MarketPost>();

            DataTable dt = new DataTable();
            dt = dal.select("", "Posts");
            /* 
               1 1 1 True 50.00 17/02/4751 3:37:07 PM True False 22/03/2013 3:37:07 PM 22/03/2013 3:37:07 PM Good 
               2 1 2 True 70.00 25/07/2099 3:38:44 PM True False 22/03/2013 3:38:44 PM 22/03/2013 3:38:44 PM Excellent 
               3 2 2 False 60.00 07/08/2040 3:43:17 PM True False 22/03/2013 3:43:17 PM 22/03/2013 3:43:17 PM   */
            
            // TODO: for code refactoring: don't use number to explicitly select columns, use column names
            foreach (DataRow row in dt.Rows)
            {
                int textBookID = (int)row[2];
                string title = getTextBookTitle(textBookID, dal);
                
                bool isBuy = ((bool)row[3] == true) ? true : false;

                string course = getCourseName(textBookID, dal);

                string condition = (row[10] is System.DBNull) ? "" : (string)row[10];

                int profileID = (int)row[1];
                string postedBy = getPostedBy(profileID, dal);

                DateTime datePosted = (DateTime)row[8];

                string isbn = getISBN(textBookID, dal);

                string author = getAuthor(textBookID, dal);

                int price = Convert.ToInt32(row[4]);

                // Marketing page, use !isBuy flag to show all sell posts
                MarketPost mpm = new MarketPost(title, !isBuy, course, condition, postedBy, datePosted, isbn, author, price);
                mpmLst.Add(mpm);
            }
            
            return View(mpmLst); 
        }
    }
}
