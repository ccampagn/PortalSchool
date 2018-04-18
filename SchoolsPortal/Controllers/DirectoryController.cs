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
            ViewBag.directory = db.getdirectory(((user)Session["user"]).getusercred().getuserid(),"","");
            return View("~/Views/Directory/directory.cshtml");
        }
        [HttpPost]
        public ActionResult DirectoryMaster(dirmodel obj)
        {
            if (Session["user"] != null)
            {
                db db = new db();
                ViewBag.directory = db.getdirectory(((user)Session["user"]).getusercred().getuserid(),obj.position,obj.grade);
                ModelState.Clear();
                return PartialView("directory");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}