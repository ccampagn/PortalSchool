
using SchoolsPortal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class ReportCardController : Controller
    {
        // GET: ReportCard
        public ActionResult Index()
        {
            db db = new db();
            ViewBag.reportcard=db.getschoolyears(((user)Session["user"]).getusercred().getuserid());
            return View();
        }
        [HttpPost]
        public ActionResult Report()
        {
            int schoolyearid = Convert.ToInt32(Request["schoolyearid"]);
            db db = new db();
           List<reportcarddisplay> courseid = db.getcourseid(schoolyearid, ((user)Session["user"]).getusercred().getuserid());
            course a = new course();
            for (int x = 0; x < courseid.Count; x++)
            {
               courseid[x].setlettergrade(db.getlettergrade(((user)Session["user"]).getusercred().getuserid(),100*a.finalcalcgrade(Convert.ToInt32(courseid[x].getcourseid()), Convert.ToInt32(Request["schoolyearid"]))));
            }
            createpdf pdf = new createpdf();
            pdf.createpass();
            //download pdf
            //get course id for grade
            //db db = new db();
            //ViewBag.reportcard = db.getschoolyears(((user)Session["user"]).getusercred().getuserid());
            return View();
        }
    }
}