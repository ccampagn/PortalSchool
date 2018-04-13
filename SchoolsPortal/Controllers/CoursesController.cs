using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolsPortal.Models;
using System.Collections;

namespace SchoolsPortal.Controllers
{
    public class CoursesController : Controller
    {
        [HttpPost]
        public ActionResult Result()
        {
            if (Session["user"] != null)
            {
                db db = new db();
                decimal score = 0;
                decimal totalpoints = 0;
                foreach (dynamic s in (((testassignment)Session["testassignment"]).getquestions()))
                {
                    totalpoints = s.getpoints() + totalpoints;
                    if (s.getcorrectanswer() == Convert.ToInt32(Request[s.getquestionid().ToString()]))
                    {
                        score = s.getpoints() + score;

                    }
                }
                ViewBag.score = score / totalpoints;
                db.insertgrade(((user)Session["user"]).getusercred().getuserid(), db.getassignmentid(((testassignment)Session["testassignment"]).gettestassignmentid()), score);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public ActionResult Test()
        {
            if (Session["user"] != null)
            {
                int id = Convert.ToInt32(Request["testsid"]);
                db db = new db();
                Session["testassignment"] = db.gettestasignment(id);
                db.insertteststatus(id, ((user)Session["user"]).getusercred().getuserid(),0);
                ViewBag.testassignment = Session["testassignment"];
                return View();
            }
            return RedirectToAction("Index", "Home");
        }



        // GET: Course
        public ActionResult Index(int coursesid,int userid)
        {
            //move to course class soonest
            db db = new db();
            ViewBag.assignment = db.getallasignment(coursesid, userid);
            course a = new course();
            ViewBag.displaygrade = a.calcdisplaygrade(coursesid, userid);
            ViewBag.finalgrade = a.calcgradedisplay(ViewBag.displaygrade);
            ViewBag.messageboard = db.getmessageboard(coursesid);
            if (userid == ((user)Session["user"]).getusercred().getuserid())
            {
                return View();
            }
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}