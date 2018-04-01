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

        public reportcarddisplay(int courseid, string coursename, string departmentname, string coursenumber, string lettergrade)
        {
            this.courseid = courseid;
            this.coursename = coursename;
            this.departmentname = departmentname;
            this.coursenumber = coursenumber;
            this.lettergrade = lettergrade;
        }

        public int getcourseid()
        {
            return courseid;
        }
        public string gmetcoursename()
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
        public void setlettergrade(string lettergrade)
        {
            this.lettergrade = lettergrade;
        }
    }
}