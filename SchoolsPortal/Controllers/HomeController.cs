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
            if (Session["user"] != null)//check if valid user
            {
                if (Session["schoolyearid"] == null)//check if first time ir if change of schoolyearid
                {
                    getdata(0);//get varies data
                }
                else
                {
                    getdata((int)Session["schoolyearid"]);//get data but use schoolyearid
                }
                return View("~/Views/Student/Home.cshtml");//go to student homepage
            }
            return View();//return to login page 
        }

        [HttpPost]
        public ActionResult schoolyear()//change of schoolyear dropdown action
        {
            Session["schoolyearid"] = Convert.ToInt32(Request["schoolyear"]);//get schoolyearid of dropdown box
            return RedirectToAction("Index", "Home");//redirect to student home page
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult LogIn()//login action
        {
            bool status = false;     //default as can't sign in     
            db db = new db();          //open access to the db
            string hash = "";//set hash to blank
            hash = db.gethash(Request.Form["username"]);//get hash basic on username
            if (hash != "")//check if hash blank
            {
                    status = PasswordHash.ValidatePassword(Request.Form["password"], hash);//check if password validate
            }
            if (status)//check if allow to signin
            {
                Session["user"] = db.getuser(Request.Form["username"]);//set session to the user
                return RedirectToAction("Index", "Home");              //redirect to home page controlleer       
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Incorrect Username/Password');</script>");  //error message             
            }
            return View();//return login view
        }
        private void getdata(int schoolyear)  //getdata for view elements of the home page
        {
            db db = new db();   //access db methods
            Session["district"] = db.getdistrictid(((user)Session["user"]).getusercred().getuserid());
            ViewBag.schoolday = db.getschoolday(((user)Session["user"]).getusercred().getuserid());//get course info forb 
            ViewBag.schoolyear = db.getschoolyear(schoolyear, ((user)Session["user"]).getusercred().getuserid());//get the list of school year
            ViewBag.filter = db.getfilterinfo(((user)Session["user"]).getusercred().getuserid());//get filter for event and newstories
            ViewBag.message = db.getmessage(((user)Session["user"]).getusercred().getuserid());//get all message for the user
            ViewBag.events = db.getevents(ViewBag.filter);//get all the different 
            ViewBag.newstories = db.getnewstories(ViewBag.filter);//get all the new stories
            ViewBag.sport = db.getsportlist(((user)Session["user"]).getusercred().getuserid(),schoolyear);//get the list of the different stories
            ViewBag.courses = db.getcourse(((user)Session["user"]).getusercred().getuserid(),schoolyear);//get list of courses for current year
           course a = new course();
           for (int x = 0; x < ViewBag.courses.Count; x++)
            {
                decimal finalgrade = a.finalcalcgrade(ViewBag.courses[x].getcourseid(), ((user)Session["user"]).getusercred().getuserid());//calc final grade for spec course
                (ViewBag.courses[x]).setgrade(Math.Round(finalgrade * 100));//set the final grade

           }
        }
    }
}