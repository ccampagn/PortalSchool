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
        private DateTime periodstart;
        private DateTime periodend;
        private decimal grade;      //grade
        public course(int courseid, string department, string coursecode, string sectioncode, string coursename, string description, name teacher, string classroom,DateTime periodstart, DateTime periodend, decimal grade)//course constructor
        {
            this.courseid = courseid;
            this.department = department;
            this.coursecode = coursecode;
            this.sectioncode = sectioncode;
            this.coursename = coursename;
            this.description = description;
            this.teacher = teacher;
            this.classroom = classroom;
            this.periodstart = periodstart;
            this.periodend = periodend;
            this.grade = grade;
        }

        public course()//blank course constructor
        {
        }

        public int getcourseid()//get course id
        {
            return courseid;
        }
        public string getdepartment()//get department
        {
            return department;
        }
        public string getcoursecode()//get course code
        {
            return coursecode;
        }
        public string getsectioncode()//get section code
        {
            return sectioncode;
        }
        public string getcoursename()//get course name
        {
            return coursename;
        }
        public string getdescription()//get description
        {
            return description;
        }
        public name getteacher()//get teacher
        {
            return teacher;
        }
        public string getclassroom()//get classroom
        {
            return classroom;
        }
        public DateTime getperiodstart()//get period
        {
            return periodstart;
        }
        public DateTime getperiodend()//get period
        {
            return periodend;
        }

        public decimal getgrade()//get grade
        {
            return grade;
        }
        public void setgrade(decimal grade)//set grade
        {
            this.grade = grade;
        }
        public List<gradedisplay> calcdisplaygrade(int course, int userid)//calc display grade return list as grade
        {
            db db = new db();
            List<assignment> assign = db.getallasignment(course, userid,DateTime.Now);//get list of all the assignment with grade for all 
            List<gradedisplay> gradedisplay = db.getgradedisplay(course);//get the grade display
            if (db.getcoursegradetype(course) == 1)//check if grade type for the course point,category, it is point here
            {
                for (int x = 0; x < gradedisplay.Count; x++)//loop thru gradedisplay
                {
                    if (gradedisplay[x].gettype() == 1)//differentquarter
                    {
                        gradedisplay[x].setpercent(db.getpercentgrade(userid, course, gradedisplay[x].getgradedisplayid(),DateTime.Now));//set grade for certain quarter
                    }
                    if (gradedisplay[x].gettype() == 2)//assignmengt not part of quarter
                    { 
                        gradedisplay[x].setpercent(db.getpercentgradecategory(userid, course, gradedisplay[x].getgradedisplayid(),DateTime.Now));//set grade for certain special assignment that are not part of a quarter
                    }
                }
            }
            else//it is category here
            {
                List<categorygrade> category = db.getcategorygrade(course);//get different category and grade percent here
                decimal point = 0;//default point
                foreach (dynamic p in (gradedisplay))//list thru on the different grade display
                {
                    if (p.gettype() == 1)//check if for a quarter
                    {
                        foreach (dynamic s in (category))//loop thru the different category
                        {
                            point = point + (s.getgradepercent() * db.getcategoriesgrade(course, userid, p.getgradedisplayid(), s.getcategoryid(),DateTime.Now));
                        }
                        p.setpercent(point);//set percent
                        point = 0;//reset percent to 0
                    }
                    if (p.gettype() == 2)////check if special assignment
                    {
                        p.setpercent(db.getpercentgradecategory(userid, course, p.getgradedisplayid(),DateTime.Now));//set grade for special assignment
                    }
                }
            }

            return gradedisplay;//return list of gradedisplay
        }
        public decimal calcgradedisplay(List<gradedisplay> gradedisplay)//calc the final grade based on quarter and special assignment grade
        {
            decimal finalgrade = 0;//default final grade
            decimal percenttotal = 0;//default percent total
            for (int x = 0; x < gradedisplay.Count; x++)//loop thru the different grade display
            {

                if (gradedisplay[x].getpercent() != -1)//check to make sure grade with valid
                {
                    percenttotal = percenttotal + gradedisplay[x].getperiodpercent() * gradedisplay[x].getpercent();//calc periodpecent time percent
                    finalgrade = finalgrade + gradedisplay[x].getperiodpercent();//add percentperiod up
                }

            }

            return percenttotal / finalgrade;//get final grade by div percenttotal by finalgrade
        }
        public decimal finalcalcgrade(int course, int userid)//method calc 
        {
            return calcgradedisplay(calcdisplaygrade(course, userid));//calcgradedisplay
        }
    }
}