﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Facebook; 

namespace BRApplication.Controllers
{
    public class HomeController : Controller
    {
        #region Index

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application. This is Kevin.";

            if (Request.Url.Host.ToLower().Equals("www.bookspade.com"))
            {
                return Redirect("http://bookspade.com"); 
            }

            return View();
        }

        #endregion

        #region InsertIt [TEST FUNCTION --> NEEDS TO BE DELETED]

        public JsonResult InsertIt(int x)
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DB1"].ToString();
            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO TestTable (Test) VALUES (@param)";

                command.Parameters.AddWithValue("@param", x);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return Json(string.Format("Successfully Inserted:{0}", x));
        }

        #endregion

        #region About

        public ActionResult About()
        {
            ViewBag.Message = "ASMA";
            
            return View();
        }

        #endregion

        #region Contact

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page. This is Johnny";

            return View();
        }

        #endregion

    }
}
