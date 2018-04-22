using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class course
    {
        private int courseid;       //courseid
        private string department;  //department
        private string coursecode;  //course code number
        private string sectioncode; //section code
        private string coursename;  //course name
        private string description; //description
        private name teacher;       //teacher
        private string classroom;   //classroom
        private string period;      //period
        private decimal grade;      //grade
        public course(int courseid, string department, string coursecode, string sectioncode, string coursename, string description, name teacher, string classroom, string period, decimal grade)
        {
            this.courseid = courseid;
            this.department = department;
            this.coursecode = coursecode;
            this.sectioncode = sectioncode;
            this.coursename = coursename;
            this.description = description;
            this.teacher = teacher;
            this.classroom = classroom;
            this.period = period;
            this.grade = grade;
        }

        public course()
        {
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
        public string getperiod()
        {
            return period;
        }

        public decimal getgrade()
        {
            return grade;
        }
        public void setgrade(decimal grade)
        {
            this.grade = grade;
        }
        public List<gradedisplay> calcdisplaygrade(int course, int userid)//calc display grade return list as grade
        {
            db db = new db();
            List<assignment> assign = db.getallasignment(course, userid);//get list of all the assignment with grade for all 
            List<gradedisplay> gradedisplay = db.getgradedisplay(course);//get the grade display
            if (db.getcoursegradetype(course) == 1)
            {
                for (int x = 0; x < gradedisplay.Count; x++)
                {
                    if (gradedisplay[x].gettype() == 1)
                    {
                        gradedisplay[x].setpercent(db.getpercentgrade(userid, course, gradedisplay[x].getgradedisplayid()));
                    }
                    if (gradedisplay[x].gettype() == 2)
                    {
                        gradedisplay[x].setpercent(db.getpercentgradecategory(userid, course, gradedisplay[x].getgradedisplayid()));
                    }
                }


            }
            else
            {
                List<categorygrade> category = db.getcategorygrade(course);
                decimal point = 0;
                foreach (dynamic p in (gradedisplay))
                {
                    if (p.gettype() == 1)
                    {
                        foreach (dynamic s in (category))
                        {
                            point = point + (s.getgradepercent() * db.getcategoriesgrade(course, userid, p.getgradedisplayid(), s.getcategoryid()));
                        }
                        p.setpercent(point);
                        point = 0;
                    }
                    if (p.gettype() == 2)
                    {
                        p.setpercent(db.getpercentgradecategory(userid, course, p.getgradedisplayid()));
                    }
                }
            }

            return gradedisplay;
        }
        public decimal calcgradedisplay(List<gradedisplay> gradedisplay)
        {
            decimal finalgrade = 0;
            decimal percenttotal = 0;
            for (int x = 0; x < gradedisplay.Count; x++)
            {

                if (gradedisplay[x].getpercent() != -1)
                {
                    percenttotal = percenttotal + gradedisplay[x].getperiodpercent() * gradedisplay[x].getpercent();
                    finalgrade = finalgrade + gradedisplay[x].getperiodpercent();
                }

            }

            return percenttotal / finalgrade;
        }
        public decimal finalcalcgrade(int course, int userid)
        {
            return calcgradedisplay(calcdisplaygrade(course, userid));
        }
    }
}