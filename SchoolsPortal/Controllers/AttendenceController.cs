using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class AttendenceController : Controller
    {
        // GET: Attendence
        public ActionResult Index()
        {
            return View();
        }
    }
}