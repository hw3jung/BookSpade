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

        public ActionResult Index()
        {
            IEnumerable<MarketPost> marketPostCollection = MarketPostHandler.getAllMarketPosts();

            return View(marketPostCollection);
        }

        [HttpPost]
        public ActionResult GetMarketPosts(string searchString)
        {
            IEnumerable<MarketPost> marketPostCollection = null;

            if (RegexUtil.isISBN(searchString)) {
                marketPostCollection = MarketPostHandler.getMarketPostsByISBN(searchString);
            } else if (RegexUtil.isCourse(searchString)) {
                marketPostCollection = MarketPostHandler.getMarketPostsByCourse(searchString);
            } else if (RegexUtil.isTitle(searchString)) {
                marketPostCollection = MarketPostHandler.getMarketPostsByTitle(searchString);
            } else if (searchString == String.Empty) {
                marketPostCollection = MarketPostHandler.getAllMarketPosts();
            }

            if (marketPostCollection == null) {
                marketPostCollection = new List<MarketPost>();
            }

            return PartialView("MarketPost_Partial", marketPostCollection);
        }

        [HttpPost]
        public ActionResult EmailSeller(string PostedBy, string textbook, int profileID, string RespondantEmail)
        {
            string accesstoken = Convert.ToString(Session["facebooktoken"]);
            FacebookClient client = new FacebookClient(accesstoken);
            
            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string Respondant = Convert.ToString(user["name"]);

            string PosterEmail = AccountHandler.getUserProfile(profileID).Email;
            bool success = MarketPostHandler.SendSellerMail(Respondant, PostedBy, textbook, RespondantEmail, PosterEmail);

            return Json(success); 
        }

        [HttpPost]
        public ActionResult EmailBuyer(string PostedBy, string textbook, int profileID, string RespondantEmail)
        {
            string accesstoken = Convert.ToString(Session["facebooktoken"]);
            FacebookClient client = new FacebookClient(accesstoken);

            IDictionary<string, object> user = (IDictionary<string, object>)client.Get("me"); //get the current user
            string Respondant = Convert.ToString(user["name"]);

            string PosterEmail = AccountHandler.getUserProfile(profileID).Email;
            bool success = MarketPostHandler.SendBuyerMail(Respondant, PostedBy, textbook, RespondantEmail, PosterEmail);

            return Json(success);
        }

        public ActionResult GetMarketPostsByisBuy(bool isBuy)
        {
            IEnumerable<MarketPost> sellPosts = MarketPostHandler.getMarketPostsByisBuy(isBuy); 
            return View("Index", sellPosts); 
        }


        [HttpPost]
        public JsonResult GetFacebookLink(int profileID)
        {
            string FBLink = AccountHandler.getUserProfile(profileID).FacebookProfileLink;
            return Json(FBLink);
        }


    }
}