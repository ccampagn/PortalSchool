﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolsPortal.Models;
using NationalParkServiceSystem.Models;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Collections;

namespace SchoolsPortal.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.test=DateTime.Now.DayOfWeek;
            db db = new db();          
            if (Session["user"] != null)//check if valid user
            {
                if (Session["schoolyearid"] == null)//check if first time in if change of schoolyearid
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
            
            string hash = db.gethash(Request.Form["username"]);//get hash basic on username
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
            Session["district"] = db.getdistrictid(((user)Session["user"]).getuserinfo().getusertype(), ((user)Session["user"]).getusercred().getuserid());//get district id
            ArrayList course = new ArrayList();
            ViewBag.type = ((user)Session["user"]).getuserinfo().getusertype();
            if (db.checkifschoolisclosed(((user)Session["user"]).getuserinfo().getusertype(),((user)Session["user"]).getusercred().getuserid()) == false)
            {
                //school is not close
                DateTime startofschoolyear = db.getstartofschoolyear(((user)Session["user"]).getusercred().getuserid(), DateTime.Now);//getfirst day of school
                if (startofschoolyear.Year != 1)//check if not default
                {
                    int numofday = (int)((DateTime.Now.Date - startofschoolyear).TotalDays - db.getnumberofdayoff(((user)Session["user"]).getuserinfo().getusertype(),startofschoolyear, DateTime.Now, ((user)Session["user"]).getusercred().getuserid()));
                    course = db.getcoursetoday(numofday, ((user)Session["user"]).getusercred().getuserid(), DateTime.Now, ((user)Session["user"]).getuserinfo().getusertype());
                }
            }        
            ViewBag.schoolday = course;
            ViewBag.schoolyear = db.getschoolyear(((user)Session["user"]).getuserinfo().getusertype(),schoolyear, ((user)Session["user"]).getusercred().getuserid());//get the list of school year
            ViewBag.message = db.getmessage(((user)Session["user"]).getusercred().getuserid());//get all message for the user
            ViewBag.events = db.getevents(((user)Session["user"]).getuserinfo().getusertype(), ((user)Session["user"]).getusercred().getuserid(), DateTime.Now);//get all the different 
            ViewBag.newstories = db.getnewstories(((user)Session["user"]).getuserinfo().getusertype(), ((user)Session["user"]).getusercred().getuserid(), DateTime.Now);//get all the new stories
            ViewBag.sport = db.getsportlist(((user)Session["user"]).getuserinfo().getusertype(),((user)Session["user"]).getusercred().getuserid(),schoolyear,DateTime.Now);//get the list of the different sport
            ViewBag.courses = db.getcourse(((user)Session["user"]).getuserinfo().getusertype(),((user)Session["user"]).getusercred().getuserid(),schoolyear,DateTime.Now);//get list of courses for current year
            course a = new course();//new course all to call method for grade
                for (int x = 0; x < ViewBag.courses.Count; x++)//loop thru all the different course in the list
                {
                    if (((user)Session["user"]).getuserinfo().getusertype() == 2)
                    {
                        ViewBag.students = db.getstudents(ViewBag.courses[x].getcourseid());
                    }
                    else
                    {
                        ArrayList list = new ArrayList();
                        list.Add(((user)Session["user"]).getusercred().getuserid());
                        ViewBag.students = list;
                    }
                    decimal finalgrade = 0;
                    if (db.checkifgraded(ViewBag.courses[x].getcourseid()))
                    {
                        for (int y = 0; y < ViewBag.students.Count; y++)
                        {
                            int userid = ViewBag.students[y];
                            finalgrade = finalgrade + a.finalcalcgrade(((user)Session["user"]).getuserinfo().getusertype(), ViewBag.courses[x].getcourseid(), userid);//calc final grade for spec course                       
                        }
                        if (ViewBag.students.Count != 0)
                        {
                            (ViewBag.courses[x]).setgrade(Math.Round(finalgrade / ViewBag.students.Count * 100));//set the final grade
                        }
                        else
                        {
                        (ViewBag.courses[x]).setgrade(-1);
                        }
                    }
                    else
                    {
                    (ViewBag.courses[x]).setgrade(-1);
                }
                }
        }
    }
}