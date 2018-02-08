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
                ViewBag.userid = ((user)Session["user"]).getusercred().getuserid();
                return View(geturl(0,0));               
            }
            return View();
        }

        [HttpGet]
        public ActionResult schoolyear(int parkname)
        {
            db db = new db();
            ViewBag.userid = ((user)Session["user"]).getusercred().getuserid();
            ViewBag.schoolyear = db.getschoolyear(parkname);
            
            return View(geturl(1,parkname));
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
                Session["user"] = db.getuser(usercred.getusername());
                ViewBag.userid = ((user)Session["user"]).getusercred().getuserid();
                ViewBag.schoolyear = db.getschoolyear(0);               
                return View(geturl(0,0));               
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Incorrect Username/Password');</script>");               
            }
            return View();
        }
        private string getipaddress()
        {
            string userip = "";
            userip = Request.UserHostAddress;
            return userip;
        }
        private string geturl(int type,int year)
        {
            db db = new db();
            if (((user)Session["user"]).getuserinfo().getusertype() == 1)
            {
                //getfilter info
                ViewBag.filter = db.getfilterinfo(((user)Session["user"]).getusercred().getuserid());
                ViewBag.message = db.getmessage(((user)Session["user"]).getusercred().getuserid());
                ViewBag.events = db.getevents(db.getdistrictid(((user)Session["user"]).getusercred().getuserid()));
                ViewBag.newstories = db.getnewstories(ViewBag.filter);
                if (type == 0)
                {
                    ViewBag.sport = db.getsportlist(((user)Session["user"]).getusercred().getuserid(),1);
                    ViewBag.courses = db.getcourse(((user)Session["user"]).getusercred().getuserid(),1);
                    
                }
                else
                {
                    ViewBag.sport = db.getsportlist(((user)Session["user"]).getusercred().getuserid(),year);
                    ViewBag.courses = db.getcourse(((user)Session["user"]).getusercred().getuserid(),year);
                }
                course a = new course();
                for (int x = 0; x < ViewBag.courses.Count; x++)
                {
                    decimal finalgrade = a.finalcalcgrade(ViewBag.courses[x].getcourseid(), ViewBag.userid);
                    (ViewBag.courses[x]).setgrade(Math.Round(finalgrade * 100));

                }
                return "~/Views/Student/Home.cshtml";
            }
            if (((user)Session["user"]).getuserinfo().getusertype() == 2)
            {
                
                ViewBag.newstories = db.getnewstories(ViewBag.filter);
                ViewBag.courses = db.getcoursestaff(((user)Session["user"]).getusercred().getuserid());
                return "~/Views/Staff/Home.cshtml";
            }
            return "";
        }
    }
}