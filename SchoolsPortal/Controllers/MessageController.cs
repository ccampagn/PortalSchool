using SchoolsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index(int messageid)
        {
            db db = new db();
            ViewBag.Message = db.getmessagethread(messageid);
            return View("~/Views/Message/message.cshtml");
        }
    }
}