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
            if (db.checkifnewstories(newstoriesid, db.getfilterinfo(((user)Session["user"]).getusercred().getuserid(),DateTime.Now),DateTime.Now))
            {
                ViewBag.news = db.getnewstoriesinfo(newstoriesid);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}