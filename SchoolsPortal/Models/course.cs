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
        private string sectioncode;
        private string coursename;
        private string description;
        private name teacher;
        private string classroom;

        private string starttime;
        private string endtime;
        private decimal grade;
        public course(int courseid,string department,string coursecode,string sectioncode,string coursename,string description,name teacher,string classroom,string starttime,string endtime,decimal grade )
        {
            this.courseid = courseid;
            this.department = department;
            this.coursecode = coursecode;
            this.sectioncode = sectioncode;
            this.coursename = coursename;
            this.description = description;
            this.teacher = teacher;
            this.classroom = classroom;
            this.starttime = starttime;
            this.endtime = endtime;
            this.grade = grade;
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
        public string getsectioncode()
        {
            return sectioncode;
        }
        public string getcoursename()
        {
            return coursename;
        }
        public string getdescription()
        {
            return description;
        }
        public name getteacher()
        {
            return teacher;
        }
        public string getclassroom()
        {
            return classroom;
        }
        public string getstarttime()
        {
            return starttime;
        }
        public string getendtime()
        {
            return endtime;
        }
        public  decimal getgrade()
        {
            return grade;
        }

        

    }
}