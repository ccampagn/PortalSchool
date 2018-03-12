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
            Session["messageid"] = messageid;
            ViewBag.Message = db.getmessagethread(messageid);
            return View("~/Views/Message/message.cshtml");
        }
        [HttpPost]
        public ActionResult MessageMaster(messmodel obj)
        {
            db db = new db();
            //insert into database
            int messageid = (int)Session["messageid"];
            db.insertmessage(((user)Session["user"]).getusercred().getuserid(),messageid, obj.text);
            ViewBag.Message = db.getmessagethread(messageid);
            ModelState.Clear();
            return PartialView("message");
        }
    }
}