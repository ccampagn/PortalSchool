using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class reportcarddisplay
    {
        private int courseid;
        private string coursename;
        private string departmentname;
        private string coursenumber;
        private string lettergrade;
        private name name;

        public reportcarddisplay(int courseid, string coursename, string departmentname, string coursenumber, string lettergrade,name name)
        {
            this.courseid = courseid;
            this.coursename = coursename;
            this.departmentname = departmentname;
            this.coursenumber = coursenumber;
            this.lettergrade = lettergrade;
            this.name = name;
        }

        public int getcourseid()
        {
            return courseid;
        }
        public string getcoursename()
        {
            return coursename;
        }
        public string getdepartment()
        {
            return departmentname;
        }
        public string getcoursenumber()
        {
            return coursenumber;
        }
        public string getlettergrade()
        {
            return lettergrade;
        }
        public name getname()
        {
            return name;
        }
        public void setlettergrade(string lettergrade)
        {
            this.lettergrade = lettergrade;
        }
    }
}