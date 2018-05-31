using SchoolsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult Index(int eventid)
        {
            db db = new db();
            if (db.checkifevent(((user)Session["user"]).getuserinfo().getusertype(),eventid,((user)Session["user"]).getusercred().getuserid(),DateTime.Now))
            {
                ViewBag.Event = db.getsingleevents(eventid);
                return View();
            }
           return RedirectToAction("Index", "Home");
        }
    }
}