using SchoolsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class NewsStoriesController : Controller
    {
        public ActionResult Index(int newstoriesid)
        {
            db db = new db();
            //   int value = Convert.ToInt32(Request["newstoriesid"]);
            ViewBag.news  = db.getnewstoriesinfo(newstoriesid);
            if (ViewBag.news == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return View();
        }
    }
}