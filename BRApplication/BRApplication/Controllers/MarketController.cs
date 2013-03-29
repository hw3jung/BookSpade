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
    }
}