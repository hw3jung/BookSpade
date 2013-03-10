using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BRApplication.Handlers;
using BRApplication.Models; 

namespace BRApplication.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/

      

        DemoHandler _DemoHandler = new DemoHandler();
        
        public ActionResult Index()
        {
            DemoModel statusModel = new DemoModel();
            statusModel.StatusID = 0;
            statusModel.StatusName = "Default Text"; 
            return View(statusModel);
        }

        [HttpPost]
        public JsonResult InsertStatus(string status)
        {
            DemoModel statusModel = new DemoModel();
            statusModel.StatusName = status;
            statusModel.StatusID = 0;

            bool success = this._DemoHandler.InsertStatus(statusModel);
            if (success)
                return Json("Successfully inserted status: " + status);
            else
                return Json("Failed to insert status: " + status); 
        }

        public ActionResult UpdatePartial(string status)
        {
            return PartialView("DemoPartial", status); 
        }

    }
}
