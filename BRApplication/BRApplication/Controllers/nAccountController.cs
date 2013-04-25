using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;

namespace BRApplication.Controllers
{
    public class nAccountController : Controller
    {
        //
        // GET: /nAccount/

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri; 
            }
        }

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "410278782401754",
                client_secret = "4d0fd841a025dd908191f50b86ec90f7",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult Login()
        {
            return View(); 
        }
    }
}
