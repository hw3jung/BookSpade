using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BRApplication.Controllers
{
    public class FacebookLoginController : Controller
    {
        //
        // GET: /FacebookLogin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FacebookLogin()
        {
            return View();
        }
    }
}
