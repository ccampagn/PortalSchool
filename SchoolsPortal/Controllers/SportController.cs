using SchoolsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class SportController : Controller
    {
        // GET: Sport
        public ActionResult Index(int sportid, int userid)
        {
            if (Session["user"] != null)
            {
                db db = new db();
                ViewBag.sportresult = db.getsportresult(sportid);
                return View("~/Views/Sport/sports.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}