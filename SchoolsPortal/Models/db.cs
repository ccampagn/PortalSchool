using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace SchoolsPortal.Models
{
    public class db
    {
        public SqlConnection openconn()
        {
            SqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=a.database.windows.net;uid=ccampagn;" +
               "pwd=aa;database=schoolsite;";
            conn = new SqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            return conn;

        }

        public ArrayList getgradeperiod(int userid)
        {
            db db = new db();
            ArrayList list = new ArrayList();            
            SqlConnection conn = db.openconn();
            String sql = "SELECT gradingperiod.gradingperiodid,gradingperiod.periodname,gradingperiod.startdate,gradingperiod.enddate FROM[dbo].[gradingperiod] join school on school.schoolid = gradingperiod.schoolid join schoolyear on schoolyear.schoolyearid = gradingperiod.schoolyearid join studentinfo on studentinfo.school = school.schoolid where startyear<GETDATE() and endyear> GETDATE() AND studentinfo.studentid = @userid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new gradeperiod(Convert.ToInt32(rdr["gradingperiodid"]), rdr["periodname"].ToString(), Convert.ToDateTime(rdr["startdate"]), Convert.ToDateTime(rdr["enddate"]),null));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }

        public ArrayList getgradedisplay(int courseid)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT periodid,periodname,type,percentage FROM [dbo].[periodpercent] join gradingperiod on gradingperiod.gradingperiodid = periodpercent.periodid where type=1 and courseid=@course UNION SELECT assignmentcategoryid,categoryname,type,percentage FROM[dbo].[periodpercent] join assignmentcategory on assignmentcategory.assignmentcategoryid = periodpercent.periodid where type= 2 and courseid = @course";
           SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@course", courseid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new gradedisplay(Convert.ToInt32(rdr["periodid"]), rdr["periodname"].ToString(), Convert.ToInt32(rdr["type"]),0, Convert.ToDecimal(rdr["percentage"])));

            }
            rdr.Close();
            db.closeconn(conn);
            return list;
        }

        public ArrayList getcourseselect(int districtid)
        {
            db db = new db();
            ArrayList list = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT gradingperiod.gradingperiodid,gradingperiod.periodname,gradingperiod.startdate,gradingperiod.enddate FROM[dbo].[gradingperiod] join school on school.schoolid = gradingperiod.schoolid join schoolyear on schoolyear.schoolyearid = gradingperiod.schoolyearid join studentinfo on studentinfo.school = school.schoolid where startyear<GETDATE() and endyear> GETDATE() AND studentinfo.studentid = @userid";
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

        public ArrayList getevents(int districtid)
        {
                db db = new db();
                ArrayList events = new ArrayList();
                SqlConnection conn = db.openconn();
            String sql = "SELECT eventid,eventtitle,description,startdate FROM [dbo].[event] where districtid = @districtid and postdate<GETDATE() and startdate>=DATEADD(dd, -1, GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@districtid", districtid);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                events.Add(new events(Convert.ToInt32(rdr["eventid"]), rdr["eventtitle"].ToString(), rdr["description"].ToString(), Convert.ToDateTime(rdr["startdate"])));

                }
                rdr.Close();
            db.closeconn(conn);
            return events;
        }

        public events getsingleevents(int eventid)
        {
            db db = new db();
            events events = null;
            SqlConnection conn = db.openconn();
            String sql = "SELECT eventid,eventtitle,description,startdate FROM [dbo].[event] where eventid = @eventid and postdate<GETDATE() and startdate>=DATEADD(dd, -1, GETDATE())";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@eventid", eventid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                events =new events(Convert.ToInt32(rdr["eventid"]), rdr["eventtitle"].ToString(), rdr["description"].ToString(), Convert.ToDateTime(rdr["startdate"]));

            }
            rdr.Close();
            db.closeconn(conn);
            return events;
        }


        public ArrayList getmessage(int userid)
        {
            db db = new db();
            ArrayList message = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "select   messagethread.messagethreadid,messagethread.threadtitle,message.datesent, message.messagetext from usermessage join messagethread on usermessage.messagethreadid = messagethread.messagethreadid join message on messagethread.messagethreadid = message.messagethreadid  where usermessage.userid=@userid order by messagethread.messagethreadid,message.datesent desc";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            int messagethread = 0;
            while (rdr.Read())
            {
                if (messagethread != Convert.ToInt32(rdr["messagethreadid"])) {
                    message.Add(new message(Convert.ToInt32(rdr["messagethreadid"]), rdr["threadtitle"].ToString(), rdr["messagetext"].ToString(), Convert.ToDateTime(rdr["datesent"]),null));
                }
                messagethread = Convert.ToInt32(rdr["messagethreadid"]);

            }
            rdr.Close();
            db.closeconn(conn);
            return message;
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
                    message.Add(new message(Convert.ToInt32(rdr["messagethreadid"]), rdr["threadtitle"].ToString(), rdr["messagetext"].ToString(), Convert.ToDateTime(rdr["datesent"]), new name(1,rdr["firstname"].ToString(),null, rdr["lastname"].ToString(),null,null,new DateTime())));
               

            }
            rdr.Close();
            db.closeconn(conn);
            return message;
        }



        public ArrayList getmessageboard(int coursesid)
        {
            db db = new db();
            ArrayList board = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT messageboardid,message,date,nameid,firstname,lastname FROM[dbo].[messageboard] join userinfo on messageboard.userid = userinfo.userid where messageboard.courseid = @courseid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@courseid", coursesid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                board.Add(new messageboard(Convert.ToInt32(rdr["messageboardid"]), new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), null, rdr["lastname"].ToString(),null,null,new DateTime()) ,Convert.ToDateTime(rdr["date"]), rdr["message"].ToString()));

            }
            rdr.Close();
            db.closeconn(conn);
            return board;
        }

        public newstories getnewstoriesinfo(int value)
        {
            db db = new db();
            newstories newstories = null;
            SqlConnection conn = db.openconn();
            String sql = "SELECT newstories.newstoriesid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,newstories.postdate,newstories.title,newstories.body FROM [dbo].[newstories] join userinfo on newstories.authorid = userinfo.userid where startdate<GETDATE() AND enddate>GETDATE() AND newstoriesid=@newstoriesid";
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



        public ArrayList getallasignment(int assignmentid, int userid)
        {
            db db = new db();
            ArrayList assignmnet = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT assignment.assignmentid,title,description,postdate,duedate,ISNULL(assignmentscorers.scores,-1) as scores,points,assignmentcategory.categoryname,CASE WHEN assignmentcategory.inquarter=0 THEN categoryname ELSE periodname END as periodname  FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid join gradingperiod on course.schoolyearid = gradingperiod.schoolyearid where assignment.sectionid = @assignmentid and (assignmentscorers.userid = @userid OR assignmentscorers.userid is NULL) and postdate<GETDATE() and duedate>gradingperiod.startdate and duedate<gradingperiod.enddate order by duedate";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@assignmentid", assignmentid);
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                assignmnet.Add(new assignment(Convert.ToInt32(rdr["assignmentid"]), rdr["title"].ToString(), rdr["periodname"].ToString(), Convert.ToDateTime(rdr["postdate"]), Convert.ToDateTime(rdr["duedate"]), Convert.ToInt32(rdr["points"]), Convert.ToInt32(rdr["scores"]), rdr["categoryname"].ToString()));
            }
            rdr.Close();
            db.closeconn(conn);
            return assignmnet;
        }

        public void logview(string ipaddress)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "INSERT INTO ipaddress (ipaddress,date) VALUES (@ipaddress,getdate())";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ipaddress", ipaddress);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }

        public void closeconn(SqlConnection conn)
        {
            conn.Close();
        }

        public user getuser(string username)//change to user class to get type, then redirect to right controller
        {
            db db = new db();

            SqlConnection conn = db.openconn();
            user user = null;
            String sql = "SELECT idpassword,username,password,nameid,firstname,middlename,lastname,suffix,sex,dateofbirth,addressid,address1,address2,city,state,zipcode,accounttype FROM password join userinfo on password.userid = userinfo.userid join address on userinfo.userid = address.userid where username = @username";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                user = new user(new usercred(Convert.ToInt32(rdr["idpassword"]), rdr["username"].ToString(), rdr["password"].ToString()), new userinfo(Convert.ToInt32(rdr["accounttype"]), new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())));
            }
            rdr.Close();
            db.closeconn(conn);
            return user;
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
        public ArrayList getcourse(int userid,int schoolyear)
        {
            db db = new db();
            ArrayList course = new ArrayList();
            SqlConnection conn = db.openconn();
            string sql = "";
            if (schoolyear == 0)
            {
                sql = "SELECT  course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,middlename,lastname,suffix,credit,classroom.classroomname,starttime,endtime,(SELECT  COALESCE(sum(scores)/sum(points)*100,0) as grade from assignment join assignmentscorers on assignment.assignmentid  = assignmentscorers.assignmentid where sectionid=course.courseid and userid=@userid) as noname FROM coursestudent join course on course.courseid = coursestudent.courseid join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid join classroom on classroom.classroomid=course.classroomid join coursetime on course.courseid=coursetime.courseid join schoolyear on course.schoolyearid = schoolyear.schoolyearid where coursestudent.studentid = @userid and startpost<GETDATE() and endpost>GETDATE() order by starttime";
            }
            else
            {
                sql = "SELECT  course.courseid,department.department,coursenumber,sectionnumber,coursename,description,firstname,middlename,lastname,suffix,credit,classroom.classroomname,starttime,endtime,(SELECT  COALESCE(sum(scores)/sum(points)*100,0) as grade from assignment join assignmentscorers on assignment.assignmentid  = assignmentscorers.assignmentid where sectionid=course.courseid and userid=@userid) as noname FROM coursestudent join course on course.courseid = coursestudent.courseid join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid join classroom on classroom.classroomid=course.classroomid join coursetime on course.courseid=coursetime.courseid join schoolyear on course.schoolyearid = schoolyear.schoolyearid where coursestudent.studentid = @userid and schoolyear.schoolyearid=@schoolyear order by starttime";
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@schoolyear", schoolyear);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
               
                course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["sectionnumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(),new name(1, rdr["firstname"].ToString(),null, rdr["lastname"].ToString(),null,null,new DateTime()), rdr["classroomname"].ToString(),  Convert.ToDateTime(rdr["starttime"]).ToShortTimeString(), Convert.ToDateTime(rdr["endtime"]).ToShortTimeString(), Convert.ToDecimal(rdr["noname"])));
            }
            rdr.Close();
            db.closeconn(conn);
            return course;
        }
        
        public ArrayList getschoolyear(int yearid)
        {
            db db = new db();
            ArrayList schoolyear = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT schoolyearid,schoolyear,startpost,endpost FROM schoolyear order by startyear";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (yearid == 0)
                {
                    if (Convert.ToDateTime(rdr["startpost"]) < DateTime.Now && Convert.ToDateTime(rdr["endpost"]) > DateTime.Now)
                    {
                        schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), true));
                    }
                    else
                    {
                        schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), false));
                    }
                }
                else
                {
                    if (Convert.ToInt32(rdr["schoolyearid"]) ==yearid)
                    {
                        schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), true));
                    }
                    else
                    {
                        schoolyear.Add(new schoolyear(Convert.ToInt32(rdr["schoolyearid"]), rdr["schoolyear"].ToString(), false));
                    }
                }

            }
            rdr.Close();
            db.closeconn(conn);
            return schoolyear;
        }
        public ArrayList getcoursestaff(int userid)
        {
            db db = new db();
            ArrayList course = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT  course.courseid,department.department,coursenumber,sectionnumber,coursename,description,credit FROM course join section on course.sectionid = section.sectionid join department on department.departmentid = section.department where course.teacherid = @teacherid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@teacherid", userid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["sectionnumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(), null,null,null,null,0));
            }
            rdr.Close();
            db.closeconn(conn);
            return course;
        }
        public ArrayList getsportlist(int userid,int year)
        {
            db db = new db();
            ArrayList sportlist = new ArrayList();
            SqlConnection conn = db.openconn();
            string sql = "";
            if (year == 0)
            {
                sql= "SELECT idsportlist,sport,levelname,seasonname,firstname,lastname,sex.sex,schoolyear FROM [dbo].[sportlist] join sport on sportlist.sportid = sport.sportid join sportlevel on sportlist.sportlevelid=sportlevel.idsportlevel join season on sportlist.seasonid =season.idseason join userinfo on sportlist.coachid=userinfo.userid join sex on sportlist.sexid = sex.sexid join schoolyear on sportlist.schoolyearid = schoolyear.schoolyearid join sportuser on sportuser.sportlistid = sportlist.idsportlist where sportuser.userid =@userid and startpost<GETDATE() and endpost>GETDATE() order by season.idseason";
                
            }
            else
            {
                sql = "SELECT idsportlist,sport,levelname,seasonname,firstname,lastname,sex.sex,schoolyear FROM [dbo].[sportlist] join sport on sportlist.sportid = sport.sportid join sportlevel on sportlist.sportlevelid=sportlevel.idsportlevel join season on sportlist.seasonid =season.idseason join userinfo on sportlist.coachid=userinfo.userid join sex on sportlist.sexid = sex.sexid join schoolyear on sportlist.schoolyearid = schoolyear.schoolyearid join sportuser on sportuser.sportlistid = sportlist.idsportlist where sportuser.userid =@userid and schoolyear.schoolyearid=@schoolyear order by season.idseason";
            }
           
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@schoolyear", year);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sportlist.Add(new sport(Convert.ToInt32(rdr["idsportlist"]),rdr["seasonname"].ToString(), rdr["sex"].ToString(), rdr["sport"].ToString(), rdr["levelname"].ToString(), new name(1, rdr["firstname"].ToString(),null, rdr["lastname"].ToString(),null,null,new DateTime())));
            }
            rdr.Close();
            db.closeconn(conn);
            return sportlist;
        }
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
        public void updatepassword(string password)
        {
            db db = new db();
            SqlConnection conn = db.openconn();
            string sql = "UPDATE Password SET password=@password where username='test'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        public string gethash(string username)
        {
            db db = new db();

            SqlConnection conn = db.openconn();
            String hash = "";
            String sql = "SELECT password FROM password where username = @username";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                hash = rdr[0].ToString();
            }
            rdr.Close();
            db.closeconn(conn);
            return hash;
        }
        public ArrayList getnewstories(int district)
        {
            db db = new db();
            ArrayList newstories = new ArrayList();
            SqlConnection conn = db.openconn();
            String sql = "SELECT newstories.newstoriesid,userinfo.firstname,userinfo.middlename,userinfo.lastname,userinfo.suffix,newstories.postdate,newstories.title,newstories.body FROM [dbo].[newstories] join userinfo on newstories.authorid = userinfo.userid where startdate<GETDATE() AND enddate>GETDATE() AND distinctid=@districtid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@districtid", district);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                newstories.Add(new newstories(Convert.ToInt32(rdr["newstoriesid"]), Convert.ToDateTime(rdr["postdate"]), rdr["title"].ToString(), rdr["body"].ToString(), new name(0, rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), null, new DateTime())));

            }
            rdr.Close();
            db.closeconn(conn);
            return newstories;
        }
        public int getdistrictid(int userid)
        {
            db db = new db();
            int districtid = 0;
            SqlConnection conn = db.openconn();
            String sql = "SELECT district.districtid FROM studentinfo join school on studentinfo.school=school.schoolid  join district on school.districtid = district.districtid where studentid=@userid";
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
        public int getstaffdistrictid(int userid)
        {
            db db = new db();
            int districtid = 0;
            SqlConnection conn = db.openconn();
            String sql = "SELECT district.districtid FROM staffinfo join school on staffinfo.school=school.schoolid  join district on school.districtid = district.districtid where staffid=@userid";
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

        public decimal getpercentgrade(int userid,int courseid,int gradingperiod)
        {
            db db = new db();
            decimal percent = 0;
            SqlConnection conn = db.openconn();
            String sql = "SELECT sum(Scores)/sum(points)  as grade FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid join gradingperiod on course.schoolyearid = gradingperiod.schoolyearid where assignment.sectionid = @courseid and (assignmentscorers.userid = @userid OR assignmentscorers.userid is NULL) and postdate<GETDATE() and duedate>gradingperiod.startdate and duedate<gradingperiod.enddate and gradingperiod.gradingperiodid=@gradeperiod and inquarter=1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@courseid", courseid);
            cmd.Parameters.AddWithValue("@gradeperiod", gradingperiod);


            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                percent = Convert.ToDecimal(rdr["grade"]);

            }
            rdr.Close();
            db.closeconn(conn);
            return percent;
        }

        public decimal getpercentgradecategory(int userid, int courseid, int category)
        {
            db db = new db();
            decimal percent = 0;
            SqlConnection conn = db.openconn();
            String sql = "SELECT ISNULL(sum(scores/points),-1) as grade FROM [dbo].[assignment] left join assignmentscorers on assignment.assignmentid = assignmentscorers.assignmentid join assignmentcategory on assignment.category = assignmentcategory.assignmentcategoryid  join course on course.courseid =assignment.sectionid  where assignment.sectionid = @courseid and (assignmentscorers.userid = @userid OR assignmentscorers.userid is NULL) and postdate<GETDATE()  and inquarter=0 and assignmentcategory.assignmentcategoryid=@category";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@courseid", courseid);
            cmd.Parameters.AddWithValue("@category", category);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                percent = Convert.ToDecimal(rdr["grade"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return percent;
        }
    }
}
