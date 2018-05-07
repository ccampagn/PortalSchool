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
            if (db.checkifevent(eventid,db.getfilterinfo(((user)Session["user"]).getusercred().getuserid())))
            {
                ViewBag.Event = db.getsingleevents(eventid);
                return View();
            }
           return RedirectToAction("Index", "Home");
        }
    }
}