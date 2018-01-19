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
        // GET: Course
        public ActionResult Index(int coursesid,int userid)
        {
            db db = new db();
            ViewBag.assignment = db.getallasignment(coursesid, userid);
            ViewBag.displaygrade = db.getgradedisplay(coursesid);
            decimal finalgrade = 0;
            decimal totalpercent = 0;
            foreach (dynamic p in (ViewBag.displaygrade))
            {
                if (p.gettype() == 1)
                {
                    p.setpercent(db.getpercentgrade(userid, coursesid, p.getgradedisplayid()));
                }
                if(p.gettype() == 2)
                {
                    p.setpercent(db.getpercentgradecategory(userid, coursesid, p.getgradedisplayid()));
                }
                if (p.getpercent() != -1)
                {
                    finalgrade = finalgrade + p.getpercent() * p.getperiodpercent();
                    totalpercent = totalpercent + p.getperiodpercent();
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