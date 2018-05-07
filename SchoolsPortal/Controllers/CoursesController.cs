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
            db db = new db();//create db object
            if (db.checkinclass(coursesid, ((user)Session["user"]).getusercred().getuserid())) {//class to make sure user have access to this page
                ViewBag.assignment = db.getallasignment(coursesid, ((user)Session["user"]).getusercred().getuserid());//get list of all the different assignment
                course a = new course();//create course object
                ViewBag.displaygrade = a.calcdisplaygrade(coursesid, ((user)Session["user"]).getusercred().getuserid());//calc display grade for all the different categories
                ViewBag.finalgrade = a.calcgradedisplay(ViewBag.displaygrade);//calc the final grade
                ViewBag.messageboard = db.getmessageboard(coursesid);//get message board based on the courseid
                return View();//return the course page
            }
            return RedirectToAction("Index", "Home");//redirect to home page if taking to access course page not part of
        }
    }
}