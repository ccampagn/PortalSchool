using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;


namespace SchoolsPortal.Models
{
    public class db
    {
        #region conn
        public SqlConnection openconn()//open db conn
        {
            SqlConnection conn;//conn variable
            string myConnectionString;//conn string
            
            conn = new SqlConnection();//create new conn
            conn.ConnectionString = myConnectionString;//setting conn
            conn.Open();//open conn to the db
            return conn;//return conn

        }
        public void closeconn(SqlConnection conn)//function to close conn
        {
            conn.Close();//conn the connection
        }
        #endregion
        #region sports
        public ArrayList getsportlist(int type,int userid, int year,DateTime date)//get the list of the different sport based on userid and schoolyear
        {
            db db = new db();//db object
            ArrayList sportlist = new ArrayList();//arraylist to return
            SqlConnection conn = db.openconn();//open conn
            string sql = "";//setting sql to blank
            if (type == 1)
            {
                if (year == 0)//default for currentyear
                {
                    sql = "SELECT idsportlist,sport,levelname,seasonname,firstname,lastname,sex.sex,schoolyear FROM [dbo].[sportlist] join sport on sportlist.sportid = sport.sportid join sportlevel on sportlist.sportlevelid=sportlevel.idsportlevel join season on sportlist.seasonid =season.idseason join userinfo on sportlist.coachid=userinfo.userid join sex on sportlist.sexid = sex.sexid join schoolyear on sportlist.schoolyearid = schoolyear.schoolyearid join sportuser on sportuser.sportlistid = sportlist.idsportlist where sportuser.userid =@userid and startpost<@date and endpost>@date order by season.idseason";//get sport for current schoolyear

                }
                else//if dropdown is select
                {
                    sql = "SELECT idsportlist,sport,levelname,seasonname,firstname,lastname,sex.sex,schoolyear FROM [dbo].[sportlist] join sport on sportlist.sportid = sport.sportid join sportlevel on sportlist.sportlevelid=sportlevel.idsportlevel join season on sportlist.seasonid =season.idseason join userinfo on sportlist.coachid=userinfo.userid join sex on sportlist.sexid = sex.sexid join schoolyear on sportlist.schoolyearid = schoolyear.schoolyearid join sportuser on sportuser.sportlistid = sportlist.idsportlist where sportuser.userid =@userid and schoolyear.schoolyearid=@schoolyear order by season.idseason";//get sport for certain schoolyear
                }

                SqlCommand cmd = new SqlCommand(sql, conn);//sql command
                cmd.Parameters.AddWithValue("@userid", userid);//add userid parameter
                cmd.Parameters.AddWithValue("@schoolyear", year);//add schoolyear parameter
                cmd.Parameters.AddWithValue("@date", date);//add schoolyear parameter
                SqlDataReader rdr = cmd.ExecuteReader();//get the data
                while (rdr.Read())//loop thru the datareader
                {
                    sportlist.Add(new sport(Convert.ToInt32(rdr["idsportlist"]), rdr["seasonname"].ToString(), rdr["sex"].ToString(), rdr["sport"].ToString(), rdr["levelname"].ToString(), new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime())));//add to list of sport
                }
                rdr.Close();//close datareader
            }
            else
            {
                if (year == 0)//default for currentyear
                {
                    sql = "SELECT idsportlist,sport,levelname,seasonname,firstname,lastname,sex.sex,schoolyear FROM [dbo].[sportlist] join sport on sportlist.sportid = sport.sportid join sportlevel on sportlist.sportlevelid=sportlevel.idsportlevel join season on sportlist.seasonid =season.idseason join userinfo on sportlist.coachid=userinfo.userid join sex on sportlist.sexid = sex.sexid join schoolyear on sportlist.schoolyearid = schoolyear.schoolyearid where coachid =@userid and startpost<@date and endpost>@date order by season.idseason";//get sport for current schoolyear

                }
                else//if dropdown is select
                {
                    sql = "SELECT idsportlist,sport,levelname,seasonname,firstname,lastname,sex.sex,schoolyear FROM [dbo].[sportlist] join sport on sportlist.sportid = sport.sportid join sportlevel on sportlist.sportlevelid=sportlevel.idsportlevel join season on sportlist.seasonid =season.idseason join userinfo on sportlist.coachid=userinfo.userid join sex on sportlist.sexid = sex.sexid join schoolyear on sportlist.schoolyearid = schoolyear.schoolyearid where coachid =@userid and schoolyear.schoolyearid=@schoolyear order by season.idseason";//get sport for certain schoolyear
                }

                SqlCommand cmd = new SqlCommand(sql, conn);//sql command
                cmd.Parameters.AddWithValue("@userid", userid);//add userid parameter
                cmd.Parameters.AddWithValue("@schoolyear", year);//add schoolyear parameter
                cmd.Parameters.AddWithValue("@date", date);//add schoolyear parameter
                SqlDataReader rdr = cmd.ExecuteReader();//get the data
                while (rdr.Read())//loop thru the datareader
                {
                    sportlist.Add(new sport(Convert.ToInt32(rdr["idsportlist"]), rdr["seasonname"].ToString(), rdr["sex"].ToString(), rdr["sport"].ToString(), rdr["levelname"].ToString(), new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime())));//add to list of sport
                }
                rdr.Close();//close datareader


            }
            db.closeconn(conn);//close connection
            return sportlist;
        }

        public bool checkifgraded(int courseid)
        {
            db db = new db();//db object
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT gradingsystem FROM [dbo].[course] where courseid =@courseid";//check if in course
            SqlCommand cmd = new SqlCommand(sql, conn);//run sql command
            cmd.Parameters.AddWithValue("@courseid", courseid);//set courseid parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            if (rdr.Read())//check if can read result
            {
                if (Convert.ToInt32(rdr["gradingsystem"]) == 0)
                {
                    return false;
                } else
                {
                    return true;
                }
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return false;//return if in course of not
        }

        public ArrayList getsportresult(int sportlistid)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT sportresultid,date,oppname,yourscore,oppscore FROM [dbo].[sportresult]  where sportlistid=@sportlistid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sportlistid", sportlistid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new sportresult(Convert.ToInt32(rdr["sportresultid"]), Convert.ToDateTime(rdr["date"]), rdr["oppname"].ToString(), Convert.ToDecimal(rdr["yourscore"]), Convert.ToDecimal(rdr["oppscore"])));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }
        #endregion
        #region course
        public ArrayList getcourse(int type, int userid, int schoolyear, DateTime date)//get course list for userid and schoolyear
        {
            db db = new db();//db object 
            ArrayList course = new ArrayList();//list of courses
            SqlConnection conn = db.openconn();//open conn
            string sql = "";//default sql statement
            if (type == 1)
            {
                if (schoolyear == 0)//onload no use dropdown menu, get schoolyear 
                {
                    sql = "SELECT course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,middlename,lastname,suffix,credit FROM coursestudent join course on course.courseid = coursestudent.courseid join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid join schoolyear on course.schoolyearid = schoolyear.schoolyearid where coursestudent.studentid = @userid and startpost<@date and endpost>@date";//select course between postdate for the schoolyear
                }
                else
                {
                    sql = "SELECT course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,middlename,lastname,suffix,credit FROM coursestudent join course on course.courseid = coursestudent.courseid join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid join schoolyear on course.schoolyearid = schoolyear.schoolyearid where coursestudent.studentid = @userid and schoolyear.schoolyearid=@schoolyear";//select course for certain schoolyear
                }
                SqlCommand cmd = new SqlCommand(sql, conn);//command
                cmd.Parameters.AddWithValue("@userid", userid);//set userid parameter
                cmd.Parameters.AddWithValue("@schoolyear", schoolyear);//set schoolyear parameter
                cmd.Parameters.AddWithValue("@date", date);
                SqlDataReader rdr = cmd.ExecuteReader();//run query
                while (rdr.Read())//read thru datareader
                {
                    course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["sectionnumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(), new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime()), "", new DateTime(), new DateTime(), 0));//add course to list               
                }
                rdr.Close();//close datareader
            }
            else
            {
                if (schoolyear == 0)//onload no use dropdown menu, get schoolyear 
                {
                    sql = "SELECT course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,middlename,lastname,suffix,credit FROM course  join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid join schoolyear on course.schoolyearid = schoolyear.schoolyearid where course.teacherid = @userid and startpost<@date and endpost>@date";//select course between postdate for the schoolyear
                }
                else
                {
                    sql = "SELECT course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,middlename,lastname,suffix,credit FROM course join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid join schoolyear on course.schoolyearid = schoolyear.schoolyearid where course.teacherid = @userid and schoolyear.schoolyearid=@schoolyear";//select course for certain schoolyear
                }
                SqlCommand cmd = new SqlCommand(sql, conn);//command
                cmd.Parameters.AddWithValue("@userid", userid);//set userid parameter
                cmd.Parameters.AddWithValue("@schoolyear", schoolyear);//set schoolyear parameter
                cmd.Parameters.AddWithValue("@date", date);
                SqlDataReader rdr = cmd.ExecuteReader();//run query
                while (rdr.Read())//read thru datareader
                {
                    course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["sectionnumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(), new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime()), "", new DateTime(), new DateTime(), 0));//add course to list               
                }
                rdr.Close();//close datareader

            }
            db.closeconn(conn);//close conn
            return course;//return list of course
        }

        public ArrayList getstudents(int courseid)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "select studentid from coursestudent where courseid=@courseid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@courseid", courseid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(Convert.ToInt32(rdr["studentid"]));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }

        public ArrayList getcourseselect(int districtid,DateTime date)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT gradingperiod.gradingperiodid,gradingperiod.periodname,gradingperiod.startdate,gradingperiod.enddate FROM[dbo].[gradingperiod] join school on school.schoolid = gradingperiod.schoolid join schoolyear on schoolyear.schoolyearid = gradingperiod.schoolyearid join studentinfo on studentinfo.school = school.schoolid where startyear<@date and endyear>@date AND studentinfo.studentid = @userid";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new gradeperiod(Convert.ToInt32(rdr["gradingperiodid"]), rdr["periodname"].ToString(), Convert.ToDateTime(rdr["startdate"]), Convert.ToDateTime(rdr["enddate"]), null));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }
        
        public List<reportcarddisplay> getcourseid(int schoolyearid, int studentid)
        {
            db db = new db();
            List<reportcarddisplay> list = new List<reportcarddisplay>();
            SqlConnection conn = db.openconn();
            String sql = "SELECT course.courseid,coursename,department.department,coursenumber,firstname,lastname FROM [dbo].[course] join coursestudent on course.courseid =coursestudent.courseid join section on section.sectionid = course.sectionid join department on section.department = department.departmentid join userinfo on userinfo.nameid = course.teacherid where schoolyearid =@schoolyearid and studentid=@studentid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@schoolyearid", schoolyearid);
            cmd.Parameters.AddWithValue("@studentid", studentid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new reportcarddisplay(Convert.ToInt32(rdr["courseid"]), rdr["coursename"].ToString(), rdr["department"].ToString(), rdr["coursenumber"].ToString(), "", new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime())));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }
        public bool checkinclass(int coursesid, int userid)//check if user is in a course
        {
            db db = new db();//db object
            bool incheck = false;//default to not in course
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT * FROM [dbo].[coursestudent] where studentid=@userid and courseid =@courseid";//check if in course
            SqlCommand cmd = new SqlCommand(sql, conn);//run sql command
            cmd.Parameters.AddWithValue("@userid", userid);//set userid parameter
            cmd.Parameters.AddWithValue("@courseid", coursesid);//set courseid parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            if (rdr.Read())//check if can read result
            {
                incheck = true;//set to true because user is in course
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return incheck;//return if in course of not
        }
        public ArrayList getcoursetoday(int schooldaynum,int userid,DateTime now,int type)
        {
            db db = new db();
            ArrayList course = new ArrayList();
            SqlConnection conn = db.openconn();
            if (type == 1)
            {
                string sql = "SELECT course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,lastname,classroomname,periodstart,periodend from  course  join section on section.sectionid = course.sectionid join coursestudent on course.courseid = coursestudent.courseid join courseperiod on course.courseid = courseperiod.courseid join period on courseperiod.periodid = period.periodid join daytype on daytype.daytypeid = period.typedayid  join type on type.typeid = daytype.scheduletypeid join userinfo on userinfo.userid = course.teacherid join classroom on classroom.classroomid = courseperiod.classroomid  join department on section.department = department.departmentid where typeid=@typeid and studentid =@userid and ((@schooldaynum % NULLIF(outofday, 0)= dayalt-1 and dateofweek=0)  or (dateofweek =@dateofweek)) order by periodstart";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@typeid", db.gettypeid(type,userid, now));
                cmd.Parameters.AddWithValue("@dateofweek", (int)now.DayOfWeek);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@schooldaynum", schooldaynum);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["sectionnumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(), new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime()), rdr["classroomname"].ToString(), DateTime.Today.Add((TimeSpan)rdr["periodstart"]), DateTime.Today.Add((TimeSpan)rdr["periodend"]), 0));
                }
                rdr.Close();
            }
            else
            {
                int schoolid = db.getschool(type, userid);
                string sql = "SELECT course.courseid,department.department,coursenumber,sectionnumber,coursename,description,classroomname,periodstart,periodend from  course  join section on section.sectionid = course.sectionid  join courseperiod on course.courseid = courseperiod.courseid join period on courseperiod.periodid = period.periodid join daytype on daytype.daytypeid = period.typedayid  join type on type.typeid = daytype.scheduletypeid join classroom on classroom.classroomid = courseperiod.classroomid  join department on section.department = department.departmentid where typeid=@typeid and teacherid =@userid and ((@schooldaynum % NULLIF(outofday, 0)= dayalt-1 and dateofweek=0)  or (dateofweek =@dateofweek)) order by periodstart";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@typeid", db.gettypeid(type,userid, now));
                cmd.Parameters.AddWithValue("@dateofweek", (int)now.DayOfWeek);
                cmd.Parameters.AddWithValue("@userid",userid);
                cmd.Parameters.AddWithValue("@schooldaynum", schooldaynum);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["sectionnumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(),null, rdr["classroomname"].ToString(), DateTime.Today.Add((TimeSpan)rdr["periodstart"]), DateTime.Today.Add((TimeSpan)rdr["periodend"]), 0));
                }
                rdr.Close();
            }
            
            
            db.closeconn(conn);
            return course;
        }

        private int gettypeid(int type,int userid,DateTime date)
        {
            db db = new db();
            int typeid = 0;
            int a = db.getschool(type, userid);
            int b = db.getdistrictid(type, userid);
            SqlConnection conn = db.openconn();
            string sql = "SELECT  type FROM [dbo].[schoolclosing] where type!=0 and date=@date and (schoolid=@school or districtid=@districtid)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@school", db.getschool(type,userid));
            cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type, userid));
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {               
                typeid = Convert.ToInt32(rdr["type"]);
            }
            else
            {
                typeid =db.getdefaulttype(db.getschool(type, userid));
            }
            rdr.Close();
            db.closeconn(conn);
            return typeid;
        }

        private int getdefaulttype(int school)
        {
            db db = new db();
            int typeid = 0;
            SqlConnection conn = db.openconn();
            string sql = "SELECT  typeid FROM [dbo].[type] where defaultvalue=1 and schoolid=@school";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@school", school);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                typeid = Convert.ToInt32(rdr["typeid"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return typeid;
        }

        public int getscheduleid(int userid)
        {
            db db = new db();
            int scheduleid = 0;
            SqlConnection conn = db.openconn();
            string sql = "SELECT scheduleid FROM studentinfo where studentid =  @userid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                scheduleid = Convert.ToInt32(rdr["scheduleid"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return scheduleid;
        }
        #endregion
        #region test
        public ArrayList getquestion(int testsid)
        {
            db db = new db();
            ArrayList questions = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT questionid,questiontext,answersid,points,type FROM [dbo].[question] where testsid = @testsid order by seqid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testsid", testsid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                questions.Add(new question(Convert.ToInt32(rdr["questionid"]), rdr["questiontext"].ToString(), db.getanswers(Convert.ToInt32(rdr["questionid"])), Convert.ToInt32(rdr["answersid"]), Convert.ToDecimal(rdr["points"]), Convert.ToInt32(rdr["type"])));
            }
            rdr.Close();
            db.closeconn(conn);
            return questions;
        }
        public ArrayList getanswers(int questionsid)
        {
            db db = new db();
            ArrayList answers = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT answersid,answertext FROM [dbo].[answers] where questionid =@questionid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@questionid", questionsid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                answers.Add(new answers(Convert.ToInt32(rdr["answersid"]), rdr["answertext"].ToString()));
            }
            rdr.Close();
            db.closeconn(conn);
            return answers;
        }
        public int testvalid(int testid)
        {
            db db = new db();
            int valid = 0;
            SqlConnection conn = db.openconn();
            String sql = "select * from teststatus where testid = @testid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testid", testid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                valid = 1;
            }
            rdr.Close();
            db.closeconn(conn);
            return valid;
        }
        public testassignment gettestasignment(int testsid)
        {
            db db = new db();

            testassignment testassignment = null;
            SqlConnection conn = db.openconn();
            String sql = "select title,testlimit from assignment join tests on assignment.assignmentid = tests.assignmentid where tests.testsid =@testsid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testsid", testsid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                testassignment = new testassignment(testsid, rdr["title"].ToString(), db.getquestion(testsid), Convert.ToInt32(rdr["testlimit"]));
            }
            rdr.Close();
            db.closeconn(conn);
            return testassignment;
        }
        public void inserttestanswer(int testid, int userid, int questionid, int answerid, string text)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "INSERT INTO answerstest (testid,userid,questionid,answerid,answertext) VALUES (@testid,@userid,@questionid,@answerid,@answertext)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testid", testid);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@questionid", questionid);
            cmd.Parameters.AddWithValue("@answerid", answerid);
            cmd.Parameters.AddWithValue("@answertext", text);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }

        public void insertteststatus(int testid, int userid,int status)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "INSERT INTO teststatus (testid,userid,status) VALUES (@testid,@userid,@status)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testid", testid);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@status", status);           
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        #endregion
        #region assignment/grade
        public void insertgrade(int userid, int assignmentid, decimal grade)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "INSERT INTO assignmentscorers (userid,assignmentid,scores) VALUES (@userid,@assignmentid,@scores)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@assignmentid", assignmentid);
            cmd.Parameters.AddWithValue("@scores", grade);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        public int getassignmentid(int testid)//change to user class to get type, then redirect to right controller
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            int assignmentid = 0;
            String sql = "SELECT assignment.assignmentid from assignment join tests on assignment.assignmentid = tests.assignmentid where testsid=@testsid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testsid", testid);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                assignmentid = Convert.ToInt32(rdr["assignmentid"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return assignmentid;
        }
        public int getgrade(int userid) { 
            db db = new db();
            SqlConnection conn = db.openconn();
            int studentid = 0;
            String sql = "SELECT grade from studentinfo where studentid=@studentid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@studentid",userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                studentid = Convert.ToInt32(rdr["grade"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return studentid;
        }
        public ArrayList getgradeperiod(int userid,DateTime date)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT gradingperiod.gradingperiodid,gradingperiod.periodname,gradingperiod.startdate,gradingperiod.enddate FROM[dbo].[gradingperiod] join school on school.schoolid = gradingperiod.schoolid join schoolyear on schoolyear.schoolyearid = gradingperiod.schoolyearid join studentinfo on studentinfo.school = school.schoolid where startyear<@date and endyear> @date AND studentinfo.studentid = @userid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@date", date);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new gradeperiod(Convert.ToInt32(rdr["gradingperiodid"]), rdr["periodname"].ToString(), Convert.ToDateTime(rdr["startdate"]), Convert.ToDateTime(rdr["enddate"]), null));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }
        public string getlettergrade(int userid, decimal grade)
        {
            db db = new db();
            string lettergrade = "";
            SqlConnection conn = db.openconn();
            String sql = "select lettergrade from graderange join studentinfo on studentinfo.school=graderange.schoolid where lowerend<=@grade AND upperend>=@grade AND studentid = @userid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@grade", grade);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lettergrade = rdr["lettergrade"].ToString();

            }
            rdr.Close();
            db.closeconn(conn);
            return lettergrade;
        }
        public List<assignment> getallasignment(int type,int assignmentid, int userid,DateTime date)
        {
            db db = new db();   //declare db object
            List<assignment> assignmnet = new List<assignment>();//list of the assignment
            SqlConnection conn = db.openconn(); //conn to db
            String sql = "SELECT assignment.assignmentid,title,assignment.description,postdate,duedate,ISNULL(assignmentscorers.scores,-1) as scores,points,assignmentcategory.categoryname,CASE WHEN assignmentcategory.inquarter=0 THEN categoryname ELSE periodname END as periodname,ISNULL(testsid,0) as testsid  FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid join section on section.sectionid = course.sectionid join gradingperiod on course.schoolyearid = gradingperiod.schoolyearid left join tests on tests.assignmentid = assignment.assignmentid where schoolid=@schoolid and assignment.sectionid = @assignmentid and (assignmentscorers.userid = @userid OR assignmentscorers.userid is NULL) and postdate<@date and duedate>gradingperiod.startdate and duedate<gradingperiod.enddate order by duedate";//sql the get all assignment,-1 for score if no score,quarter,also if have test 
            SqlCommand cmd = new SqlCommand(sql, conn);//setup commend
            cmd.Parameters.AddWithValue("@assignmentid", assignmentid); //setting the courseid
            cmd.Parameters.AddWithValue("@userid", userid);             //setting the userid
            cmd.Parameters.AddWithValue("@schoolid", db.getschool(type,userid));
            cmd.Parameters.AddWithValue("@date", date);
            SqlDataReader rdr = cmd.ExecuteReader();//datareader
            while (rdr.Read())//read result
            {
                assignmnet.Add(new assignment(Convert.ToInt32(rdr["assignmentid"]), rdr["title"].ToString(), rdr["periodname"].ToString(), Convert.ToDateTime(rdr["postdate"]), Convert.ToDateTime(rdr["duedate"]), Convert.ToInt32(rdr["points"]), Convert.ToInt32(rdr["scores"]), rdr["categoryname"].ToString(), Convert.ToInt32(rdr["testsid"]), db.testvalid(Convert.ToInt32(rdr["testsid"]))));//add to assignment
            }
            rdr.Close();//close result
            db.closeconn(conn);//close conn
            return assignmnet;//return list of assignment with grade
        }
        public List<gradedisplay> getgradedisplay(int courseid)//get what different quarter and special category wealth
        {
            db db = new db();//db object
            List<gradedisplay> list = new List<gradedisplay>();//list of display grade
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT periodid,periodname,type,percentage FROM [dbo].[periodpercent] join gradingperiod on gradingperiod.gradingperiodid = periodpercent.periodid where type=1 and periodpercent.courseid=@course UNION SELECT assignmentcategoryid,categoryname,type,percentage FROM[dbo].[periodpercent] join assignmentcategory on assignmentcategory.assignmentcategoryid = periodpercent.periodid where type= 2 and periodpercent.courseid = @course";//sql get value for each quarter and assignment category
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@course", courseid);//course parameter
            SqlDataReader rdr = cmd.ExecuteReader();//datareader
            while (rdr.Read())//read entry from datareader
            {
                list.Add(new gradedisplay(Convert.ToInt32(rdr["periodid"]), rdr["periodname"].ToString(), Convert.ToInt32(rdr["type"]), 0, Convert.ToDecimal(rdr["percentage"])));//add entry for each category and special assignment

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//conn close
            return list;//return list of category
        }
        public int getcoursegradetype(int courseid)//get type grading per course(points or percent)
        {
            db db = new db();//db object
            int type = 0;//default type
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT gradingsystem FROM [dbo].[course]  where courseid = @courseid";//sql get gradesystem
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@courseid", courseid);//set courseid parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            while (rdr.Read())//sql result loop thru
            {
                type = Convert.ToInt32(rdr["gradingsystem"]);//set type 

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return type;//return course grade type
        }
        public decimal getpercentgrade(int userid, int courseid, int gradingperiod,DateTime date)//get grade for grading period
        {
            db db = new db();//db object
            decimal percent = 0;//default percent 0
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT isnull(sum(Scores)/sum(points),-1)  as grade FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid join gradingperiod on course.schoolyearid = gradingperiod.schoolyearid where assignment.sectionid = @courseid and (assignmentscorers.userid = @userid OR assignmentscorers.userid is NULL) and postdate<@date and duedate>gradingperiod.startdate and duedate<gradingperiod.enddate and gradingperiod.gradingperiodid=@gradeperiod and inquarter=1";//sql query get grade for quarter
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@userid", userid);//set userid parameter
            cmd.Parameters.AddWithValue("@courseid", courseid);//set courseid parameter
            cmd.Parameters.AddWithValue("@gradeperiod", gradingperiod);//set 
            cmd.Parameters.AddWithValue("@date", date);//set 
            SqlDataReader rdr = cmd.ExecuteReader();//datareader
            while (rdr.Read())
            {
                percent = Convert.ToDecimal(rdr["grade"]);//set grade

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return percent;//return percent as grade
        }
        public decimal getpercentgradecategory(int userid, int courseid, int category,DateTime date)//get special assisnment grade
        {
            db db = new db();//db object
            decimal percent = 0;//default percent
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT ISNULL(sum(scores/points),-1) as grade FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid  where assignment.sectionid = @courseid and (assignmentscorers.userid = @userid OR assignmentscorers.userid is NULL) and postdate<@date  and inquarter=0 and assignmentcategory.assignmentcategoryid=@category";//sql query to get special category grade
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@userid", userid);//setup userid parameter
            cmd.Parameters.AddWithValue("@courseid", courseid);//setup courseid parameter
            cmd.Parameters.AddWithValue("@category", category);//setup category parameter
            cmd.Parameters.AddWithValue("@date", date);//setup category parameter
            SqlDataReader rdr = cmd.ExecuteReader();//run query
            while (rdr.Read())
            {
                percent = Convert.ToDecimal(rdr["grade"]);//set grade to result
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return percent;//return the grade for the special assignment
        }
        public List<categorygrade> getcategorygrade(int courseid)//get different category for certain course
        {
            db db = new db();//db object
            List<categorygrade> category = new List<categorygrade>();//list for categorygrade
            SqlConnection conn = db.openconn();//open db conn
            String sql = "SELECT categorygradeid,gradepercent,categoryid FROM [dbo].[categorygrade] where courseid = @course";//sql query get different 
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@course", courseid);//setup courseid parameter
            SqlDataReader rdr = cmd.ExecuteReader();//excute sql
            while (rdr.Read())//read dataread while
            {
                category.Add(new categorygrade(Convert.ToInt32(rdr["categorygradeid"]), Convert.ToDecimal(rdr["gradepercent"]), Convert.ToInt32(rdr["categoryid"])));//add to list of categorygrade

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close db conn
            return category;//return list of different categorygrade
        }
        public decimal getcategoriesgrade(int course, int user, int gradeperiod, int category,DateTime date)//get grade for certain categories within gradeperiod
        {
            db db = new db();//db object
            decimal percent = 0;//default percent to 0
            SqlConnection conn = db.openconn();//db open conn
            String sql = "SELECT ISNULL(sum(Scores)/sum(points),-1) as grade  FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid join gradingperiod on course.schoolyearid = gradingperiod.schoolyearid where assignment.sectionid = @course and (assignmentscorers.userid = @user OR assignmentscorers.userid is NULL) and postdate<@date and duedate>gradingperiod.startdate and duedate<gradingperiod.enddate and gradingperiod.gradingperiodid=@gradeperiod and inquarter=1 and assignmentcategoryid =@category";//query get grade for certain categories
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@user", user);//setup user parameter
            cmd.Parameters.AddWithValue("@course", course);//setup course parameter
            cmd.Parameters.AddWithValue("@gradeperiod", gradeperiod);//setup gradeperiod parameter
            cmd.Parameters.AddWithValue("@category", category);//setup category parameter
            cmd.Parameters.AddWithValue("@date", date);//setup category parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            while (rdr.Read())
            {
                percent = Convert.ToDecimal(rdr["grade"]);//set percent based on result

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return percent;//return grade
        }
        #endregion
        #region schoolday/year
        public ArrayList getschoolyears(int userid,DateTime date)
        {
            db db = new db();
            ArrayList schoolyear = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT schoolyearid,schoolyear FROM studentinfo join school on school.schoolid = studentinfo.school join schoolyear on school.districtid = schoolyear.districtid where endyear<@date and studentid=@userid order by startyear";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@date", date);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), false));
            }
            rdr.Close();
            db.closeconn(conn);
            return schoolyear;
        }
        public bool checkifcoursesport(int type,int schoolyearid, int userid)//check if schoolyear have course/sport
        {
            db db = new db();//db object
            bool check = false;//default to false
            SqlConnection conn = db.openconn();//open conn
            String sql = "";
            if (type == 1)
            {
                sql = "SELECT  userid FROM [dbo].[sportlist] join sportuser on  sportlist.idsportlist = sportuser.sportlistid where schoolyearid =@schoolyearid and userid=@userid UNION select studentid from course join coursestudent on course.courseid = coursestudent.courseid where schoolyearid = @schoolyearid and studentid = @userid";//sql check for sport and course for certain sportyearid
            }
            else
            {
                sql = "SELECT  coachid FROM [dbo].[sportlist] where schoolyearid =@schoolyearid and coachid=@userid UNION select teacherid from course where schoolyearid = @schoolyearid and teacherid = @userid";//sql check for sport and course for certain sportyearid
            }
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@userid", userid);//userid parameter
            cmd.Parameters.AddWithValue("@schoolyearid", schoolyearid);//schoolyearid parameter
            SqlDataReader rdr = cmd.ExecuteReader();// get data from query
            if (rdr.Read())//check if can read data
            {
                check = true;//true if query return anything
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return check;//return true/false
        }
        public ArrayList getschoolyear(int type,int yearid, int userid)//get list of schoolyear based on usedid,yearid use to selection current one
        {
            db db = new db();//db object
            ArrayList schoolyear = new ArrayList();//list for schoolyear
            SqlConnection conn = db.openconn();//open db conn
            String sql = "SELECT schoolyearid,schoolyear,startpost,endpost FROM schoolyear where districtid = @districtid  order by startyear";//sql query get school year
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type,userid));//setting parameter
            SqlDataReader rdr = cmd.ExecuteReader();//run sql statement
            while (rdr.Read())//run for each record
            {
                if (db.checkifcoursesport(type,Convert.ToInt32(rdr["schoolyearid"]), userid))//check uf have course or sport info for userid and schoolyear
                {
                    if (yearid == 0)//check if yearid is 0, first load is 0 and default to current schoolyear
                    {
                        if (Convert.ToDateTime(rdr["startpost"]) < DateTime.Now && Convert.ToDateTime(rdr["endpost"]) > DateTime.Now)//add schoolyear with selection if currentyear
                        {

                            schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), true));//add schoolyear with true 
                        }
                        else//else if not current schoolyear
                        {
                            schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), false));//add schoolyear with false
                        }
                    }
                    else//if year was selected by dropdown menu
                    {
                        if (Convert.ToInt32(rdr["schoolyearid"]) == yearid)//if yearid is the one selected
                        {
                            schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), true));//add schoolyear with selected
                        }
                        else
                        {
                            schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), false));//add schoolyear not selected
                        }
                    }
                }

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return schoolyear;//list of the different schoolyear and if selected
        }
        public int getnumberofdayoff(int type,DateTime start, DateTime cur,int userid)//get number of day off in a range
        {
            db db = new db();//db object
            SqlConnection conn = db.openconn();//open conn
            int numofclos = 0;//number of day close
            String sql = "SELECT COUNT(*) as numofclos FROM [dbo].[schoolclosing] where date>@startdate and date<@enddate and ((districtid =@districtid) or (schoolid=@schoolid and districtid=0))";//sql get number of close day between 2 dates
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@startdate", start);//add parameter
            cmd.Parameters.AddWithValue("@enddate", cur);//add parameter
            cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type,userid));//add parameter
            cmd.Parameters.AddWithValue("@schoolid", db.getschool(type,userid));//add parameter
            SqlDataReader rdr = cmd.ExecuteReader();//run query
            if (rdr.Read())//get result
            {
                numofclos = Convert.ToInt32(rdr["numofclos"]);//set numofclos
            }
            rdr.Close();//close data reader
            db.closeconn(conn);//close conn
            return numofclos;//return numofclos
        }
        public DateTime getstartofschoolyear(int userid,DateTime date)///get first day of school
        {
            db db = new db();//db object
            SqlConnection conn = db.openconn();//open db conn
            DateTime startofseasonyear = new DateTime();//new datetime
            String sql = "SELECT startyear from schoolyear join school on school.districtid = schoolyear.districtid join studentinfo on school.schoolid = studentinfo.studentid where studentid =1 and startyear<@date and endyear>@date";
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@userid", userid);//set up parameter
            cmd.Parameters.AddWithValue("@date", date);//set up parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            if (rdr.Read())//read the startyear
            {
                startofseasonyear = Convert.ToDateTime(rdr["startyear"]);//setting starting of the year
            }
            rdr.Close();//close data reader
            db.closeconn(conn);//close conn 
            return startofseasonyear;//return first date of startyear
        }
        public int gettypeofday(int userid)//get the type of day 
        {
            db db = new db();//db object
            SqlConnection conn = db.openconn();//open conn
            int dayalt = 0;//default day alt
            String sql = "SELECT dayalt FROM [dbo].[school] join studentinfo on studentinfo.school = school.schoolid where studentid =@userid";//query to get day alt
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@userid", userid);//add parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data read
            if (rdr.Read())//read result
            {
                dayalt = Convert.ToInt32(rdr["dayalt"]);//set day alt
            }
            rdr.Close();//close command
            db.closeconn(conn);//close conn
            return dayalt;//return dayalt
        }
        public bool checkifschoolisclosed(int type,int userid)//get course for today based on userid
        {           
            db db = new db();//db object
            bool list = false;
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT COUNT(*) as closing FROM [dbo].[schoolclosing] where type=0 and date=@date and (schoolclosing.districtid=@districtid or schoolclosing.schoolid=@schoolid)";
            SqlCommand cmd = new SqlCommand(sql, conn);//set up command
            cmd.Parameters.AddWithValue("@date",DateTime.Now.Date);//add parameter
            cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type,userid));//add parameter
            cmd.Parameters.AddWithValue("@schoolid", db.getschool(type,userid));//add parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            if (rdr.Read())//reader data
            {
                if (Convert.ToInt32(rdr["closing"]) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return list;
        }
        #endregion
        #region school
        public ArrayList getdirectory(int type,int userid, string position, string grade)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            int schoolid = db.getschool(type,userid);
            String sql = "";

            if (position == "Staff")
            {
                sql = "SELECT nameid, firstname, middlename,lastname,suffix,department.department,cardid,sex.sex,dateofbirth,position.position FROM [dbo].[userinfo] join staffinfo on userinfo.userid = staffinfo.staffid  join sex on userinfo.sex = sex.sexid join position on staffinfo.position = position.positionid join department on staffinfo.department =department.departmentid where school=@schoolid";
            }
            else if (position == "Student")
            {
                sql = "SELECT nameid, firstname, middlename, lastname, suffix, grade.grade as department,cardid,sex.sex,dateofbirth,'Student' as position FROM[dbo].[userinfo] join studentinfo on userinfo.userid = studentinfo.studentid join grade on studentinfo.grade = grade.gradeid join sex on userinfo.sex = sex.sexid where school = @schoolid";
            }
            else
            {

                sql = "SELECT nameid, firstname, middlename,lastname,suffix,department.department,cardid,sex.sex,dateofbirth,position.position FROM [dbo].[userinfo] join staffinfo on userinfo.userid = staffinfo.staffid  join sex on userinfo.sex = sex.sexid join position on staffinfo.position = position.positionid join department on staffinfo.department =department.departmentid where school=@schoolid union SELECT nameid,firstname,middlename,lastname,suffix,grade.grade,cardid,sex.sex,dateofbirth,'Student' FROM[dbo].[userinfo] join studentinfo on userinfo.userid = studentinfo.studentid join grade on studentinfo.grade = grade.gradeid join sex on userinfo.sex = sex.sexid where school = @schoolid";
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@schoolid", db.getschool(type,userid));
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new directory(Convert.ToInt32(rdr["nameid"]), new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), rdr["department"].ToString(), rdr["cardid"].ToString(), rdr["position"].ToString()));
            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }
        public int getschool(int type,int userid)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            int schoolid = 0;
            if (type == 1)
            {
                String sql = "select school from studentinfo where studentid = @userid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    schoolid = Convert.ToInt32(rdr["school"]);
                }
                rdr.Close();
            } else
            {
                String sql = "select school from staffinfo where staffid = @userid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    schoolid = Convert.ToInt32(rdr["school"]);
                }
                rdr.Close();
            }
            db.closeconn(conn);
            return schoolid;
        }
        public ArrayList getallposition(int type,int userid)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            list.Add(new position(0, "Student"));
            SqlConnection conn = db.openconn();
            String sql = "SELECT positionid,position FROM[dbo].[position] where distrinctid = @districtid;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type,userid));
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new position(Convert.ToInt32(rdr["positionid"]), rdr["position"].ToString()));
            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }
        #endregion
        #region message
        public void insertmessageboard(int userid,int courses, string text)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "INSERT INTO messageboard (message,userid,date,courseid) VALUES (@message,@userid,@date,@courses)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@message", text);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@courses", courses);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        public ArrayList getmessagethread(int messagethreadid)
        {
            db db = new db();
            ArrayList message = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "select messagethread.messagethreadid,messagethread.threadtitle,message.datesent, message.messagetext,userinfo.firstname,userinfo.lastname from usermessage join messagethread on usermessage.usermessageid = messagethread.messagethreadid join message on messagethread.messagethreadid = message.messagethreadid join userinfo on message.messfrom = userinfo.userid where messagethread.messagethreadid =@messagethreadid order by message.datesent";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@messagethreadid", messagethreadid);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                message.Add(new message(Convert.ToInt32(rdr["messagethreadid"]), rdr["threadtitle"].ToString(), rdr["messagetext"].ToString(), Convert.ToDateTime(rdr["datesent"]), new name(1, rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime())));


            }
            rdr.Close();
            db.closeconn(conn);
            return message;
        }
        public ArrayList getmessageboard(int coursesid)//get messageboard for each course
        {
            db db = new db();//db object
            ArrayList board = new ArrayList();//arraylist for all the messageboard
            SqlConnection conn = db.openconn();//open db conn
            String sql = "SELECT messageboardid,message,date,nameid,firstname,lastname FROM[dbo].[messageboard] join userinfo on messageboard.userid = userinfo.userid where messageboard.courseid = @courseid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@courseid", coursesid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                board.Add(new messageboard(Convert.ToInt32(rdr["messageboardid"]), new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), null, rdr["lastname"].ToString(), null, null, new DateTime()), Convert.ToDateTime(rdr["date"]), rdr["message"].ToString()));

            }
            rdr.Close();
            db.closeconn(conn);
            return board;
        }
        public ArrayList getmessage(int userid)//get message for certain user
        {
            db db = new db();//db object
            ArrayList message = new ArrayList();//arraylist for different message
            SqlConnection conn = db.openconn();//open conn
            String sql = "select messagethread.messagethreadid,messagethread.threadtitle,message.datesent, message.messagetext from usermessage join messagethread on usermessage.messagethreadid = messagethread.messagethreadid join message on messagethread.messagethreadid = message.messagethreadid  where usermessage.userid=@userid order by messagethread.messagethreadid,message.datesent desc";//get different user message
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@userid", userid);//set user parameter
            SqlDataReader rdr = cmd.ExecuteReader();//datareader
            int messagethread = 0;//default messafethreadid
            while (rdr.Read())//loop thru user message
            {
                if (messagethread != Convert.ToInt32(rdr["messagethreadid"]))
                {
                    message.Add(new message(Convert.ToInt32(rdr["messagethreadid"]), rdr["threadtitle"].ToString(), rdr["messagetext"].ToString(), Convert.ToDateTime(rdr["datesent"]), null));//get last message for home screen
                }
                messagethread = Convert.ToInt32(rdr["messagethreadid"]);//set messagethread id

            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return message;//return message list
        }
        public void insertmessage(int userid, int messageid, string text,DateTime date)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "INSERT INTO message (datesent,messagethreadid,messfrom,messagetext) VALUES (@date,@messagethreadid,@messfrom,@messagetext)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date",date);
            cmd.Parameters.AddWithValue("@messagethreadid", messageid);
            cmd.Parameters.AddWithValue("@messfrom", userid);
            cmd.Parameters.AddWithValue("@messagetext", text);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        
        #endregion
        #region events
        public bool checkifevent(int eventid,filterclass filter,DateTime date)
        {
            db db = new db();//db object
            ArrayList events = new ArrayList();//arraylist of the different events
            SqlConnection conn = db.openconn();//open conn
            String sql = "SELECT * FROM ( SELECT event.eventid,event.eventtitle,event.description,event.startdate,  ROW_NUMBER() OVER(PARTITION BY event.eventid ORDER BY event.startdate) rn FROM Event join eventdisplay on event.eventid = eventdisplay.eventid join display on eventdisplay.displayid = display.displayid where event.eventid=@eventid and districtid= @districtid and postdate<@date and startdate>=DATEADD(dd, -1, @date) AND(courseid = 0";//get event sql query
            for (int x = 0; x < filter.getcourse().Count; x++)//loop thru courses
            {
                sql = sql + " OR courseid = @course" + x;//add possible course
            }
            sql = sql + ") AND(sectionid = 0";//add sectionid info
            for (int x = 0; x < filter.getsection().Count; x++)//loop thru section
            {
                sql = sql + " OR sectionid = @section" + x;//add possible section
            }
            sql = sql + ") AND(teacherid = 0";//add teacher info
            for (int x = 0; x < filter.getteacher().Count; x++)//loop thru teacger
            {
                sql = sql + " OR teacherid = @teacher" + x;//add possible teach
            }
            sql = sql + ") ) a WHERE rn = 1 ";//more sql code
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@eventid", eventid);
            cmd.Parameters.AddWithValue("@districtid", filter.getdistrict());//add district parameter
            cmd.Parameters.AddWithValue("@schoolid", filter.getschool());//add school parameter
            cmd.Parameters.AddWithValue("@gradeid", filter.getgrade());//add grade 
            for (int x = 0; x < filter.getcourse().Count; x++)//add course parameter
            {
                cmd.Parameters.AddWithValue("@course" + x, filter.getcourse()[x]);
            }
            for (int x = 0; x < filter.getsection().Count; x++)//add section paraameter
            {
                cmd.Parameters.AddWithValue("@section" + x, filter.getsection()[x]);
            }
            for (int x = 0; x < filter.getteacher().Count; x++)//add teacher parameter
            {
                cmd.Parameters.AddWithValue("@teacher" + x, filter.getteacher()[x]);
            }
            SqlDataReader rdr = cmd.ExecuteReader();//datareader
            if (rdr.Read())
            {
                return true;//return true if an event match
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return false;//return as not event available to use
        }
        public ArrayList getevents(int type,int userid,DateTime date)//get event using the filter
        {
            db db = new db();//db object
            ArrayList events = new ArrayList();//arraylist of the different events
            SqlConnection conn = db.openconn();//open conn
            if (type == 1)
            {
                String sql = "SELECT * FROM " +
                    "( SELECT event.eventid,event.eventtitle,event.description,event.startdate,  ROW_NUMBER() " +
                    "OVER(PARTITION BY event.eventid ORDER BY event.startdate) rn FROM Event " +
                    "join eventdisplay on event.eventid = eventdisplay.eventid " +
                    "join display on eventdisplay.displayid = display.displayid " +
                    "where (postdate<@date and startdate>=DATEADD(dd, -1, @date)) and" +
                    "(districtid = @districtid) and " +
                    "(schoolid = 0 or schoolid = @schoolid) and" +
                    "(gradeid = 0 or gradeid = @gradeid) and" +
                    "(typeid = 0 or typeid =1) and" +
                    "(schoolyearid = 0 or schoolyearid = @schoolid) and" +
                    "(courseid IN(select course.courseid from course join coursestudent on course.courseid = coursestudent.courseid where coursestudent.studentid = @userid) or courseid = 0) and" +
                    "(sectionid IN(select section.sectionid from section join course on section.sectionid = course.courseid join coursestudent on course.courseid = coursestudent.courseid where coursestudent.studentid = @userid) or sectionid = 0) and" +
                    "(teacherid IN(select teacherid from course join coursestudent on course.courseid = coursestudent.courseid where coursestudent.studentid = @userid) or teacherid = 0 )) a WHERE rn = 1 ";//more sql code
                SqlCommand cmd = new SqlCommand(sql, conn);//command
                cmd.Parameters.AddWithValue("@userid", userid);//add district parameter
                cmd.Parameters.AddWithValue("@date", date);//add district parameter
                cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type, userid));//add district parameter
                cmd.Parameters.AddWithValue("@schoolid", db.getschool(type, userid));//add school parameter
                cmd.Parameters.AddWithValue("@gradeid", db.getgrade(userid));//add grade 
                SqlDataReader rdr = cmd.ExecuteReader();//datareader
                while (rdr.Read())
                {
                    events.Add(new events(Convert.ToInt32(rdr["eventid"]), rdr["eventtitle"].ToString(), rdr["description"].ToString(), Convert.ToDateTime(rdr["startdate"])));//add to event list
                }
                rdr.Close();//close datareader
            }
            else
            {
                String sql = "SELECT * FROM " +
                    "( SELECT event.eventid,event.eventtitle,event.description,event.startdate,  ROW_NUMBER() " +
                    "OVER(PARTITION BY event.eventid ORDER BY event.startdate) rn FROM Event " +
                    "join eventdisplay on event.eventid = eventdisplay.eventid " +
                    "join display on eventdisplay.displayid = display.displayid " +
                    "where (postdate<@date and startdate>=DATEADD(dd, -1, @date)) and" +
                    "(districtid = @districtid) and " +
                    "(schoolid = 0 or schoolid = @schoolid) and" +
                    "(typeid = 0 or typeid =2) and" +
                     "(departmentid = 0 or departmentid =@department) and" +
                    "(schoolyearid = 0 or schoolyearid = @schoolid) and" +
                    "(courseid IN(select course.courseid from course where course.teacherid = @userid) or courseid = 0) and" +
                    "(sectionid IN(select section.sectionid from section join course on section.sectionid = course.courseid where course.teacherid = @userid) or sectionid = 0) and" +
                    "(teacherid = @userid or teacherid = 0 )) a WHERE rn = 1 ";//more sql code
                SqlCommand cmd = new SqlCommand(sql, conn);//command
                cmd.Parameters.AddWithValue("@userid", userid);//add district parameter
                cmd.Parameters.AddWithValue("@date", date);//add district parameter
                cmd.Parameters.AddWithValue("@department", db.getdepartmentid(userid));//add district parameter
                cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type, userid));//add district parameter
                cmd.Parameters.AddWithValue("@schoolid", db.getschool(type, userid));//add school parameter
                SqlDataReader rdr = cmd.ExecuteReader();//datareader
                while (rdr.Read())
                {
                    events.Add(new events(Convert.ToInt32(rdr["eventid"]), rdr["eventtitle"].ToString(), rdr["description"].ToString(), Convert.ToDateTime(rdr["startdate"])));//add to event list
                }
                rdr.Close();//close datareader
            }
            db.closeconn(conn);//close conn
            return events;//return list of event
        }

        private int getdepartmentid(int userid)
        {
            db db = new db();//db class
            SqlConnection conn = db.openconn();//open conn
            int department = 0;//set hash to blank
            String sql = "SELECT department FROM [dbo].[staffinfo] where staffid=@userid";//query to get the hash value
            SqlCommand cmd = new SqlCommand(sql, conn);//setup command
            cmd.Parameters.AddWithValue("@userid", userid);//setting parameter
            SqlDataReader rdr = cmd.ExecuteReader();//run query
            if (rdr.Read())//read first entry if available
            {
                department = Convert.ToInt32(rdr[0]);//setting hash variable
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close connection
            return department;//return hash value
        }

        public events getsingleevents(int eventid)
        {
            db db = new db();
            events events = null;
            SqlConnection conn = db.openconn();
            String sql = "SELECT eventid,eventtitle,description,startdate FROM [dbo].[event] where eventid = @eventid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@eventid", eventid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                events = new events(Convert.ToInt32(rdr["eventid"]), rdr["eventtitle"].ToString(), rdr["description"].ToString(), Convert.ToDateTime(rdr["startdate"]));

            }
            rdr.Close();
            db.closeconn(conn);
            return events;
        }
        #endregion             
        #region newstories
        public bool checkifnewstories(int newstories,filterclass filter,DateTime date)//get all newstories using filter
        {
            db db = new db();//db object
            SqlConnection conn = db.openconn();//db open conn
            String sql = "SELECT * FROM " +
                "( SELECT newstories.newstoriesid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,newstories.postdate,newstories.title,newstories.body,  ROW_NUMBER() " +
                "OVER(PARTITION BY newstories.newstoriesid ORDER BY newstories.postdate) rn FROM newstories " +
                "join newstoriesdisplay on newstories.newstoriesid = newstoriesdisplay.newstoriesid " +
                "join display on newstoriesdisplay.displayid = display.displayid " +
                "join userinfo on newstories.authorid = userinfo.userid " +
                "where newstories.newstoriesid =@newstoriesid and " +
                "districtid = @districtid and postdate<@date and startdate<@date AND enddate>@date AND(display.courseid = 0";//sql get newstories
            for (int x = 0; x < filter.getcourse().Count; x++)//loop thru course
            {
                sql = sql + " OR display.courseid = @course" + x;//course parameter
            }
            sql = sql + ") AND(display.sectionid = 0";//sectionid filter
            for (int x = 0; x < filter.getsection().Count; x++)//loop thru section
            {
                sql = sql + " OR display.sectionid = @section" + x;//sectionparameter
            }
            sql = sql + ") AND(display.teacherid = 0";//teacheridfilter
            for (int x = 0; x < filter.getteacher().Count; x++)//loop thru teacher
            {
                sql = sql + " OR display.teacherid = @teacher" + x;//teacherparameter
            }
            sql = sql + ") ) a WHERE rn = 1 ";
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@districtid", filter.getdistrict());//parameter set district
            cmd.Parameters.AddWithValue("@schoolid", filter.getschool());//pparameter set school
            cmd.Parameters.AddWithValue("@gradeid", filter.getgrade());//parameter set grade
            cmd.Parameters.AddWithValue("@newstoriesid", newstories);//parameter set grade
            for (int x = 0; x < filter.getcourse().Count; x++)//loop thru course parameter
            {
                cmd.Parameters.AddWithValue("@course" + x, filter.getcourse()[x]);
            }
            for (int x = 0; x < filter.getsection().Count; x++)//loop thru section parameter
            {
                cmd.Parameters.AddWithValue("@section" + x, filter.getsection()[x]);
            }
            for (int x = 0; x < filter.getteacher().Count; x++)//loop thru teacher parameter
            {
                cmd.Parameters.AddWithValue("@teacher" + x, filter.getteacher()[x]);//
            }
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
           if (rdr.Read())
            {
                return true;
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return false;
        }
        public ArrayList getnewstories(int type,int userid,DateTime date)//get all newstories using filter
        {
            db db = new db();//db object
            ArrayList newstories = new ArrayList();//list of newstories
            SqlConnection conn = db.openconn();//db open conn  
            if (type == 1)
            {
                String sql = "SELECT * FROM " +
                    "( SELECT newstories.newstoriesid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,newstories.postdate,newstories.title,newstories.body,  ROW_NUMBER() " +
                    "OVER(PARTITION BY newstories.newstoriesid ORDER BY newstories.postdate) rn FROM newstories join newstoriesdisplay on newstories.newstoriesid = newstoriesdisplay.newstoriesid " +
                    "join display on newstoriesdisplay.displayid = display.displayid " +
                    "join userinfo on newstories.authorid = userinfo.userid " +
                    "where " +
                        "(districtid = @districtid) and " +
                        "(schoolid = 0 or schoolid = @schoolid) and" +
                        "(gradeid = 0 or gradeid = @gradeid) and" +
                        "(typeid = 0 or typeid =1) and" +
                        "(schoolyearid = 0 or schoolyearid = @schoolid) and" +
                        "(courseid IN(select course.courseid from course join coursestudent on course.courseid = coursestudent.courseid where coursestudent.studentid = @userid) or courseid = 0) and" +
                        "(sectionid IN(select section.sectionid from section join course on section.sectionid = course.courseid join coursestudent on course.courseid = coursestudent.courseid where coursestudent.studentid = @userid) or sectionid = 0) and" +
                        "(teacherid IN(select teacherid from course join coursestudent on course.courseid = coursestudent.courseid where coursestudent.studentid = @userid) or teacherid = 0 )) a WHERE rn = 1 ";//more sql code
                SqlCommand cmd = new SqlCommand(sql, conn);//command
                cmd.Parameters.AddWithValue("@userid", userid);//add district parameter
                cmd.Parameters.AddWithValue("@date", date);//add district parameter
                cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type, userid));//add district parameter
                cmd.Parameters.AddWithValue("@schoolid", db.getschool(type, userid));//add school parameter
                cmd.Parameters.AddWithValue("@gradeid", db.getgrade(userid));//add grade 
                SqlDataReader rdr = cmd.ExecuteReader();//datareader


                while (rdr.Read())
                {
                    newstories.Add(new newstories(Convert.ToInt32(rdr["newstoriesid"]), Convert.ToDateTime(rdr["postdate"]), rdr["title"].ToString(), rdr["body"].ToString(), new name(0, rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), null, new DateTime())));//add newstories to arraylist
                }
                rdr.Close();//close datareader
            }
            else
            {
                String sql = "SELECT * FROM " +
                    "( SELECT newstories.newstoriesid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,newstories.postdate,newstories.title,newstories.body,  ROW_NUMBER() " +
                    "OVER(PARTITION BY newstories.newstoriesid ORDER BY newstories.postdate) rn FROM newstories join newstoriesdisplay on newstories.newstoriesid = newstoriesdisplay.newstoriesid " +
                    "join display on newstoriesdisplay.displayid = display.displayid " +
                    "join userinfo on newstories.authorid = userinfo.userid " +
                    "where " +"(districtid = @districtid) and " +
                    "(schoolid = 0 or schoolid = @schoolid) and" +
                    "(typeid = 0 or typeid =2) and" +
                     "(departmentid = 0 or departmentid =@department) and" +
                    "(schoolyearid = 0 or schoolyearid = @schoolid) and" +
                    "(courseid IN(select course.courseid from course where course.teacherid = @userid) or courseid = 0) and" +
                    "(sectionid IN(select section.sectionid from section join course on section.sectionid = course.courseid where course.teacherid = @userid) or sectionid = 0) and" +
                    "(teacherid = @userid or teacherid = 0 )) a WHERE rn = 1 ";//more sql code
                SqlCommand cmd = new SqlCommand(sql, conn);//command
                cmd.Parameters.AddWithValue("@userid", userid);//add district parameter
                cmd.Parameters.AddWithValue("@date", date);//add district parameter
                cmd.Parameters.AddWithValue("@districtid", db.getdistrictid(type, userid));//add district parameter
                cmd.Parameters.AddWithValue("@schoolid", db.getschool(type, userid));//add school parameter              
                cmd.Parameters.AddWithValue("@department", db.getdepartmentid(userid));//add district parameter
                SqlDataReader rdr = cmd.ExecuteReader();//datareader


                while (rdr.Read())
                {
                    newstories.Add(new newstories(Convert.ToInt32(rdr["newstoriesid"]), Convert.ToDateTime(rdr["postdate"]), rdr["title"].ToString(), rdr["body"].ToString(), new name(0, rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), null, new DateTime())));//add newstories to arraylist
                }
                rdr.Close();//close datareader
            }
            db.closeconn(conn);//close conn
            return newstories;//return arraylist of newstories
        }
        public newstories getnewstoriesinfo(int value)
        {
            db db = new db();
            newstories newstories = null;
            SqlConnection conn = db.openconn();
            String sql = "SELECT newstories.newstoriesid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,newstories.postdate,newstories.title,newstories.body FROM [dbo].[newstories] join userinfo on newstories.authorid = userinfo.userid where newstoriesid=@newstoriesid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@newstoriesid", value);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                newstories = new newstories(Convert.ToInt32(rdr["newstoriesid"]), Convert.ToDateTime(rdr["postdate"]), rdr["title"].ToString(), rdr["body"].ToString(), new name(0, rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), null, new DateTime()));

            }
            rdr.Close();
            db.closeconn(conn);
            return newstories;
        }
        #endregion                              
        #region signin
        public string gethash(string username)//gethash by username
        {
            db db = new db();//db class
            SqlConnection conn = db.openconn();//open conn
            String hash = "";//set hash to blank
            String sql = "SELECT password FROM password where username = @username";//query to get the hash value
            SqlCommand cmd = new SqlCommand(sql, conn);//setup command
            cmd.Parameters.AddWithValue("@username", username);//setting parameter
            SqlDataReader rdr = cmd.ExecuteReader();//run query
            if (rdr.Read())//read first entry if available
            {
                hash = rdr[0].ToString();//setting hash variable
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close connection
            return hash;//return hash value
        }
        public user getuser(string username)//get user information using username and set the session
        {
            db db = new db();                   //create db object
            SqlConnection conn = db.openconn(); //open conn
            user user = null;                   //set user to null
            String sql = "SELECT idpassword,username,password,nameid,firstname,middlename,lastname,suffix,sex,dateofbirth,addressid,address1,address2,city,state,zipcode,accounttype FROM password join userinfo on password.userid = userinfo.userid join address on userinfo.userid = address.userid where username = @username";//sql query 
            SqlCommand cmd = new SqlCommand(sql, conn);//setup command
            cmd.Parameters.AddWithValue("@username", username);//setting parameter
            SqlDataReader rdr = cmd.ExecuteReader();//run query
            if (rdr.Read())//read first entry if available
            {
                user = new user(new usercred(Convert.ToInt32(rdr["idpassword"]), rdr["username"].ToString(), rdr["password"].ToString()), new userinfo(Convert.ToInt32(rdr["accounttype"]), new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())));//setting user object
            }
            rdr.Close();//close datareader
            db.closeconn(conn);//close connection
            return user;//return user 
        }
        #endregion
        #region district/school
        public ArrayList getallstaff()
        {
            db db = new db();
            ArrayList staffinfo = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "select userinfo.nameid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,userinfo.dateofbirth,staffinfo.cardid,address.addressid,address.address1,address.address2,address.city,address.state,address.zipcode,sex.sex,school.schoolid,school.school,department.department,position.position,tenure.tenure,staffinfo.hireddate,password.username from userinfo join staffinfo on staffinfo.staffid = userinfo.userid join address on address.addressid = userinfo.userid join sex on sex.sexid = userinfo.sex join school on school.schoolid = staffinfo.school join password on password.userid = userinfo.userid join department on department.departmentid = staffinfo.department join position on position.positionid = staffinfo.position join tenure on tenure.tenureid = staffinfo.tenure";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                staffinfo.Add(new staff(new usercred(0, rdr["username"].ToString(), null), new userinfo(0, new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())), rdr["school"].ToString(), rdr["department"].ToString(), rdr["position"].ToString(), rdr["cardid"].ToString(), rdr["tenure"].ToString(), Convert.ToDateTime(rdr["hireddate"])));

            }
            rdr.Close();
            db.closeconn(conn);
            return staffinfo;
        }
        public ArrayList getallstudents()
        {
            db db = new db();
            ArrayList studentinfo = new ArrayList();
            SqlConnection conn = db.openconn();//add userid and password 
            String sql = "select userinfo.nameid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,userinfo.dateofbirth,studentinfo.cardid,address.addressid,address.address1,address.address2,address.city,address.state,address.zipcode,sex.sex,school.schoolid,school.school,grade.grade,password.username from userinfo join studentinfo on studentinfo.studentinfoid = userinfo.userid join address on address.addressid = userinfo.userid join sex on sex.sexid = userinfo.sex join school on school.schoolid = studentinfo.school join grade on grade.gradeid = studentinfo.grade join password on password.userid=userinfo.userid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                studentinfo.Add(new student(new usercred(0, rdr["username"].ToString(), null), new userinfo(0, new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())), rdr["school"].ToString(), rdr["grade"].ToString(), rdr["cardid"].ToString()));

            }
            rdr.Close();
            db.closeconn(conn);
            return studentinfo;
        }
        public int getdistrictid(int type,int userid)//get district per userid
        {
            db db = new db();//create db object
            int districtid = 0;//default districtid
            if (type == 1)
            {
                SqlConnection conn = db.openconn();//open db conn
                String sql = "SELECT district.districtid FROM studentinfo join school on studentinfo.school=school.schoolid  join district on school.districtid = district.districtid where studentid=@userid";//sql query
                SqlCommand cmd = new SqlCommand(sql, conn);//setup command
                cmd.Parameters.AddWithValue("@userid", userid);//add parameter
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    districtid = Convert.ToInt32(rdr["districtid"]);

                }
                rdr.Close();
                db.closeconn(conn);
            }
            else
            {
                SqlConnection conn = db.openconn();//open db conn
                String sql = "SELECT district.districtid FROM staffinfo join school on staffinfo.school=school.schoolid  join district on school.districtid = district.districtid where staffid=@userid";//sql query
                SqlCommand cmd = new SqlCommand(sql, conn);//setup command
                cmd.Parameters.AddWithValue("@userid", userid);//add parameter
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    districtid = Convert.ToInt32(rdr["districtid"]);

                }
                rdr.Close();
                db.closeconn(conn);
            }
            
            
            return districtid;
        }
        public int getstaffdistrictid(int userid)//get district per staffid
        {
            db db = new db();//create db object
            int districtid = 0;//default districtid
            SqlConnection conn = db.openconn();//open db conn
            String sql = "SELECT district.districtid FROM staffinfo join school on staffinfo.school=school.schoolid  join district on school.districtid = district.districtid where staffid=@userid";//sql query
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                districtid = Convert.ToInt32(rdr["districtid"]);

            }
            rdr.Close();
            db.closeconn(conn);
            return districtid;
        }
        #endregion
        #region filter
        
        public filterclass getfilterinfo(int student,DateTime date)//get filter values for student using their studentid number
        {
            db db = new db();//db object
            SqlConnection conn = db.openconn();//open conn
            ArrayList course = new ArrayList();//list of courses
            ArrayList teacher = new ArrayList();//list of student teacher
            ArrayList section = new ArrayList();//list of section number
            int grade = 0;//grade default
            int school = 0;//school default
            int districtid = 0;//district default
            filterclass filter;//filter class
            String sql = "SELECT * FROM [dbo].[coursestudent] join  course on course.courseid = coursestudent.courseid join studentinfo on coursestudent.studentid = studentinfo.studentid join school on studentinfo.school=school.schoolid join schoolyear on schoolyear.schoolyearid = course.schoolyearid where coursestudent.studentid=@student and  @date >startpost and @date<endpost"; 
            SqlCommand cmd = new SqlCommand(sql, conn);//command
            cmd.Parameters.AddWithValue("@date", date);//set student parameter
            cmd.Parameters.AddWithValue("@student", student);//set student parameter
            SqlDataReader rdr = cmd.ExecuteReader();//data reader
            while (rdr.Read())//loop thru result
            {
                districtid = Convert.ToInt32(rdr["districtid"]);//set districtid
                course.Add(Convert.ToInt32(rdr["courseid"]));//add course to list
                teacher.Add(Convert.ToInt32(rdr["teacherid"]));//add teacher to list
                section.Add(Convert.ToInt32(rdr["sectionid"]));//add section to list
                grade = Convert.ToInt32(rdr["grade"]);//set grade
                school = Convert.ToInt32(rdr["school"]);//set school
            }
            filter = new filterclass(districtid, school, grade, course, section, teacher);//setting filter class
            rdr.Close();//close datareader
            db.closeconn(conn);//close conn
            return filter;//return filter object
        }
        #endregion        
    }
}
