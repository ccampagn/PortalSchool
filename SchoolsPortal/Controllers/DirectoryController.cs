using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolsPortal.Models;

namespace SchoolsPortal.Controllers
{
    public class DirectoryController : Controller
    {
        // GET: Directory
        public ActionResult Index()
        {
            db db = new db();
            ViewBag.directory = db.getdirectory(((user)Session["user"]).getusercred().getuserid());
            return View();
        }
    }
}