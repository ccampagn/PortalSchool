using SchoolsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class CourseSelectController : Controller
    {
        // GET: CourseSelect
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                db db = new db();
                ViewBag.courses = db.getcoursepick(1);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
    