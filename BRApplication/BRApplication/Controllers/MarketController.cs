using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Models; 

namespace BRApplication.Controllers
{
    public class MarketController : Controller
    {
        //
        // GET: /Market/

        public ActionResult Index()
        {
            List<MarketPostModel> mpmLst = new List<MarketPostModel>();
            MarketPostModel mpm1 = new MarketPostModel("My Awesome AFM 101 Textbook", false, "AFM 101", "Excellent", "Asma Patel", DateTime.Now, "978-3-16-148410-0", "Mark Twain", 300);
            MarketPostModel mpm2 = new MarketPostModel("Buy this book, buy it NAO!", false, "MATH 135", "Good", "Johnny Depp", DateTime.Now, "978-3-16-148410-0", "John Jacob JingleHeimer", 150.99);
            MarketPostModel mpm3 = new MarketPostModel("This is a nice lookin title isnt it ;)", false, "STAT 230", "Good", "Jack Johnson", DateTime.Now, "978-3-16-148410-0", "Kiets d a", 0.99);
            MarketPostModel mpm4 = new MarketPostModel("Title #4 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm5 = new MarketPostModel("Title #5 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm6 = new MarketPostModel("Title #6 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm7 = new MarketPostModel("Title #7 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm8 = new MarketPostModel("Title #8 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm9 = new MarketPostModel("Title #9 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm10 = new MarketPostModel("Title #10 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm11 = new MarketPostModel("Title #11 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm12 = new MarketPostModel("Title #12 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm13 = new MarketPostModel("Title #13 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm14 = new MarketPostModel("Title #14 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm15 = new MarketPostModel("Title #15 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm16 = new MarketPostModel("Title #16 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm17 = new MarketPostModel("Title #17 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            MarketPostModel mpm18 = new MarketPostModel("Title #18 ", false, "ACTS 230", "Good", "Hugh Grant", DateTime.Now, "978-3-16-148410-0", "Homer J. Simpson", 500.99);
            mpmLst.Add(mpm1);
            mpmLst.Add(mpm2);
            mpmLst.Add(mpm3);
            mpmLst.Add(mpm4);
            mpmLst.Add(mpm5);
            mpmLst.Add(mpm6);
            mpmLst.Add(mpm7);
            mpmLst.Add(mpm8);
            mpmLst.Add(mpm9);
            mpmLst.Add(mpm10);
            mpmLst.Add(mpm11);
            mpmLst.Add(mpm12);
            mpmLst.Add(mpm13);
            mpmLst.Add(mpm14);
            mpmLst.Add(mpm15);
            mpmLst.Add(mpm16);
            mpmLst.Add(mpm17);
            mpmLst.Add(mpm18);

            return View(mpmLst); 
        }

    }
}
