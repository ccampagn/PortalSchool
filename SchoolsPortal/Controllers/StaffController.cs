using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolsPortal.Models;

namespace SchoolsPortal.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            db db = new db();
            ViewBag.staffs = db.getallstaff();
            return View();
        }
    }
}