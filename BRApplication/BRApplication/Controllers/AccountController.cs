using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BRApplication.Filters;
using BRApplication.Models;
using BRApplication.Handlers;
using Facebook;

namespace BRApplication.Controllers
{

    public class AccountController : Controller
    {

        #region Redirect Uri

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

        #endregion

        #region Facebook

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
               // Production : 
                //client_id = "410278782401754",
                //client_secret = "4d0fd841a025dd908191f50b86ec90f7",
               // Development : 
                client_id = "424967934259582",
                client_secret = "7d491f9e46f04614240c0043094fd2d5",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        #endregion

        #region FacebookCallBack

        public ActionResult FacebookCallback(string code, string returnUrl)
        {
          
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                // Production : 
                //client_id = "410278782401754",
                //client_secret = "4d0fd841a025dd908191f50b86ec90f7",
                // Development : 
                client_id = "424967934259582",
                client_secret = "7d491f9e46f04614240c0043094fd2d5",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            IDictionary<string, object> user = (IDictionary<string, object>)fb.Get("me"); //get the current user

            UserProfile userProfile = new UserProfile(
                Convert.ToString(user["name"]),
                Convert.ToString(user["link"]),
                Convert.ToString(user["id"]),
                Convert.ToString(user["gender"]),
                Convert.ToString(user["email"]), String.Empty);

            AccountHandler.AddUser(userProfile);

            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(Convert.ToString(user["name"]), false);
            
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Login

        public ActionResult Login() //string returnUrl
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region LogOff
        //
        // POST: /Account/LogOff

        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Remove("AccessToken");
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region ManagePosts

        public ActionResult ManagePosts()
        {
            string AccessToken = Convert.ToString(Session["AccessToken"]);
            FacebookClient fb = new FacebookClient(AccessToken);
            IDictionary<string, object> user = (IDictionary<string, object>)fb.Get("me"); //get the current user
            
            UserProfile profile = AccountHandler.getUserProfile_Facebook(Convert.ToString(user["id"]));

            IEnumerable<MarketPost> marketPosts = MarketPostHandler.getMarketPostsByProfile(profile.ProfileID); 

            return View(marketPosts); 
        }

        #endregion

        #region DeletePost

        public JsonResult DeletePost(int postID)
        {
            bool success = MarketPostHandler.deletePost(postID); 
            return Json(success); 
        }

        #endregion

    }
}
