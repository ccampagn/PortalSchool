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


        public ActionResult Index(int eventid,int userid)
        {
            db db = new db();
            ViewBag.Event = db.getsingleevents(eventid);           
            return View("~/Views/Events/event.cshtml");
        }
    }
}