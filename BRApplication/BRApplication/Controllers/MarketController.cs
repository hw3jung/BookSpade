using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Models;
using BRApplication.Handlers;
using BRApplication.Utility;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Facebook;

namespace BRApplication.Controllers
{
    public class MarketController : Controller
    {
        //
        // GET: /Market/

        #region Index

        public ActionResult Index(bool isBuy)
        {
            IEnumerable<MarketPost> marketPostCollection = MarketPostHandler.getAllMarketPosts(isBuy);

            return View(marketPostCollection);
        }

        #endregion

        #region GetMarketPosts

        [HttpPost]
        public ActionResult GetMarketPosts(bool isBuy, string searchString = "")
        {
            IEnumerable<MarketPost> marketPostCollection = null;

            if (RegexUtil.isISBN(searchString)) 
            {
                marketPostCollection = MarketPostHandler.getMarketPostsByISBN(isBuy, searchString);
            } 
            else if (RegexUtil.isCourse(searchString)) 
            {
                marketPostCollection = MarketPostHandler.getMarketPostsByCourse(isBuy, searchString);

                // In case the search string is a substring of a title even though it matches the regex for courses
                if (marketPostCollection.Count() == 0)
                {
                    marketPostCollection = MarketPostHandler.getMarketPostsByTitle(isBuy, searchString);
                }
            }
            else if (RegexUtil.isTitle(searchString)) 
            {
                marketPostCollection = MarketPostHandler.getMarketPostsByTitle(isBuy, searchString);

                // In case the search string is a substring of a course even though it does not match the regex 
                // (the regex for courses only catches strings of alphabets followed by numbers, eg. "AFM 101" and not "AFM")
                if (marketPostCollection.Count() == 0)
                {
                    marketPostCollection = MarketPostHandler.getMarketPostsByCourse(isBuy, searchString);
                }
            }
            else if (searchString == String.Empty) 
            {
                marketPostCollection = MarketPostHandler.getAllMarketPosts(isBuy);
            }

            if (marketPostCollection == null) 
            {
                marketPostCollection = new List<MarketPost>();
            }

            return PartialView("MarketPost_Partial", marketPostCollection);
        }

        #endregion

        #region EmailSeller

        [HttpPost]
        public ActionResult EmailSeller(string PostedBy, string textbook, int profileID, string RespondantEmail)
        {
            string accesstoken = Convert.ToString(Session["AccessToken"]);
            FacebookClient client = new FacebookClient(accesstoken);
            
            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string Respondant = Convert.ToString(user["name"]);

            string PosterEmail = AccountHandler.getUserProfile(profileID).Email;
            bool success = MarketPostHandler.SendSellerMail(Respondant, PostedBy, textbook, RespondantEmail, PosterEmail);

            return Json(success); 
        }

        #endregion

        #region EmailBuyer

        [HttpPost]
        public ActionResult EmailBuyer(string PostedBy, string textbook, int profileID, string RespondantEmail)
        {
            string accesstoken = Convert.ToString(Session["AccessToken"]);
            FacebookClient client = new FacebookClient(accesstoken);

            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string Respondant = Convert.ToString(user["name"]);

            string PosterEmail = AccountHandler.getUserProfile(profileID).Email;
            bool success = MarketPostHandler.SendBuyerMail(Respondant, PostedBy, textbook, RespondantEmail, PosterEmail);

            return Json(success);
        }

        #endregion

        #region GetFacebookLink

        [HttpPost]
        public JsonResult GetFacebookLink(int profileID)
        {
            string FBLink = AccountHandler.getUserProfile(profileID).FacebookProfileLink;
            return Json(FBLink);
        }

        #endregion
    }
}