using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolsPortal.Models;
using NationalParkServiceSystem.Models;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace SchoolsPortal.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            db db = new db();
            string ipaddress = getipaddress();
            if (ipaddress != "")
            {
                db.logview(ipaddress);
            }
            if (Session["user"] != null)
            {
                ViewBag.courses = db.getcourse();
                return View(geturl());
                
            }
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult LogIn()
        {
            bool status = false;          
            db db = new db();
            usercred usercred = new usercred(0, Request.Form["username"], Request.Form["password"]);
            string hash = "";
            hash = db.gethash(usercred.getusername());
            if (hash != "")
            {
                    status = PasswordHash.ValidatePassword(usercred.getpassword(), hash);
            }

            if (status)
            {
                ViewBag.courses = db.getcourse();
                Session["user"] = db.getuser(usercred.getusername());
                return View(geturl());
                
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Incorrect Username/Password');</script>");               
            }
            return RedirectToAction("Index", "Home");
        }
        private string getipaddress()
        {
            string userip = "";
            userip = Request.UserHostAddress;
            return userip;
        }
        private string geturl()
        {
            if (((user)Session["user"]).getuserinfo().getusertype() == 1)
            {
                return "~/Views/Student/Home.cshtml";
            }
            if (((user)Session["user"]).getuserinfo().getusertype() == 0)
            {
                return "~/Views/Staff/Home.cshtml";
            }
            return "";
        }
    }
}