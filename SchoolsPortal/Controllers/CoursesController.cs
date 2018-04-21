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
                bool uploadgrade = true;
                foreach (dynamic s in (((testassignment)Session["testassignment"]).getquestions()))
                {
                    if (s.gettype() == 1)
                    {
                        if (Request[s.getquestionid().ToString()] == null)
                        {
                            db.inserttestanswer(((testassignment)Session["testassignment"]).gettestassignmentid(), ((user)Session["user"]).getusercred().getuserid(), s.getquestionid(),-1, "");
                        }
                        else
                        {
                            db.inserttestanswer(((testassignment)Session["testassignment"]).gettestassignmentid(), ((user)Session["user"]).getusercred().getuserid(), s.getquestionid(), Convert.ToInt32(Request[s.getquestionid().ToString()]), "");
                        }                        
                        totalpoints = s.getpoints() + totalpoints;
                        if (s.getcorrectanswer() == Convert.ToInt32(Request[s.getquestionid().ToString()]))
                        {
                            score = s.getpoints() + score;
                        }
                    }
                    else
                    {
                        uploadgrade = false;
                        db.inserttestanswer(((testassignment)Session["testassignment"]).gettestassignmentid(), ((user)Session["user"]).getusercred().getuserid(), s.getquestionid(), 0, Request[s.getquestionid().ToString()]);
                    }
                }
                ViewBag.score = score / totalpoints;
                if (uploadgrade)
                {
                    db.insertgrade(((user)Session["user"]).getusercred().getuserid(), db.getassignmentid(((testassignment)Session["testassignment"]).gettestassignmentid()), score);
                }
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
            public ActionResult Index(int coursesid)
        {
            //move to course class soonest
            db db = new db();
            int userid = ((user)Session["user"]).getusercred().getuserid();
            if (db.checkinclass(coursesid, userid)) {
                ViewBag.assignment = db.getallasignment(coursesid, userid);
                course a = new course();
                ViewBag.displaygrade = a.calcdisplaygrade(coursesid, userid);
                ViewBag.finalgrade = a.calcgradedisplay(ViewBag.displaygrade);
                ViewBag.messageboard = db.getmessageboard(coursesid);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}