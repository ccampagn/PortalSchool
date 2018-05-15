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
            if (Session["user"] != null)
            {
                db db = new db();
                Session["messageid"] = messageid;
                ViewBag.Message = db.getmessagethread(messageid);
                return View("~/Views/Message/message.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult MessageMaster(messmodel obj)
        {
            if (Session["user"] != null)
            {
                db db = new db();
                //insert into database
                int messageid = (int)Session["messageid"];
                db.insertmessage(((user)Session["user"]).getusercred().getuserid(), messageid, obj.text,DateTime.Now);
                ViewBag.Message = db.getmessagethread(messageid);
                ModelState.Clear();
                return PartialView("message");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}