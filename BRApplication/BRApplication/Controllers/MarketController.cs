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

        public ActionResult Index()
        {
            IEnumerable<MarketPost> marketPostCollection = MarketPostHandler.getAllMarketPosts();

            return View(marketPostCollection);
        }
    }
}