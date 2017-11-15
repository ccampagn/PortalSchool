using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class course
    {
        private int courseid;
        private string department;
        private string coursecode;
        private string coursename;
        private string description;
        private string teacher;

        public course(int courseid,string department,string coursecode,string coursename,string description,string teacher)
        {
            this.courseid = courseid;
            this.department = department;
            this.coursecode = coursecode;
            this.coursename = coursename;
            this.description = description;
            this.teacher = teacher;
        }

        public int getcourseid()
        {
            return courseid;
        }
        public string getdepartment()
        {
            return department;
        }
        public string getcoursecode()
        {
            return coursecode;
        }
        public string getcoursename()
        {
            return coursename;
        }
        public string getdescription()
        {
            return description;
        }
        public string getteacher()
        {
            return teacher;
        }

    }
}