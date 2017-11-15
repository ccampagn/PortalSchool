using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolsPortal.Models;

namespace SchoolsPortal.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {

            db db = new db();
            ViewBag.students=db.getallstudents();
            if (Session["user"] != null)
            {
                if (((user)Session["user"]).getuserinfo().getusertype() == 1)
                {
                    return View("~/Views/Student/Home.cshtml");
                }
                if (((user)Session["user"]).getuserinfo().getusertype() == 0)
                {
                    return View("~/Views/Staff/Home.cshtml");
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}