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
            ViewBag.score = score/totalpoints;
            db.insertgrade(((user)Session["user"]).getusercred().getuserid(), db.getassignmentid(((testassignment)Session["testassignment"]).gettestassignmentid()), score);
           return View();
        }



        [HttpPost]
        public ActionResult Test()
        {

            int id = Convert.ToInt32(Request["testsid"]);
            db db = new db();
            Session["testassignment"] = db.gettestasignment(id);
            ViewBag.testassignment = Session["testassignment"];
            return View();
        }



        // GET: Course
        public ActionResult Index(int coursesid,int userid)
        {
            //move to course class soonest
            db db = new db();
            ViewBag.assignment = db.getallasignment(coursesid, userid);
            ViewBag.displaygrade = db.getgradedisplay(coursesid);
            decimal finalgrade = 0;
            decimal totalpercent = 0;


            if (db.getcoursegradetype(coursesid) == 1)
            {
                foreach (dynamic p in (ViewBag.displaygrade))
                {
                    if (p.gettype() == 1)
                    {
                        p.setpercent(db.getpercentgrade(userid, coursesid, p.getgradedisplayid()));
                    }
                    if (p.gettype() == 2)
                    {
                        p.setpercent(db.getpercentgradecategory(userid, coursesid, p.getgradedisplayid()));
                    }
                    if (p.getpercent() != -1)
                    {
                        finalgrade = finalgrade + p.getpercent() * p.getperiodpercent();
                        totalpercent = totalpercent + p.getperiodpercent();
                    }
                }
            }
            else
            {
                ViewBag.category =db.getcategorygrade(coursesid);
                decimal point = 0;
                foreach (dynamic p in (ViewBag.displaygrade))
                {
                    if (p.gettype() == 1)
                    {
                        foreach (dynamic s in (ViewBag.category))
                        {
                            point=point+(s.getgradepercent()*db.getcategoriesgrade(coursesid, userid, p.getgradedisplayid(), s.getcategoryid()));
                        }
                        p.setpercent(point);
                        point = 0;
                    }
                    if (p.gettype() == 2)
                    {
                        p.setpercent(db.getpercentgradecategory(userid, coursesid, p.getgradedisplayid()));
                    }
                    if (p.getpercent() != -1)
                    {
                        finalgrade = finalgrade + p.getpercent() * p.getperiodpercent();
                        totalpercent = totalpercent + p.getperiodpercent();
                    }
                }
            }
            ViewBag.finalgrade = finalgrade/totalpercent;
            ViewBag.messageboard = db.getmessageboard(coursesid);
            if (userid == ((user)Session["user"]).getusercred().getuserid())
            {
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}