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

            myConnectionString = "server=abc;" +
                "pwd=abc;database=schoolsite;";
            conn = new SqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            return conn;

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
            user user=null;
            String sql = "SELECT idpassword,username,password,nameid,firstname,middlename,lastname,suffix,sex,dateofbirth,addressid,address1,address2,city,state,zipcode,accounttype FROM password join userinfo on password.userid = userinfo.userid join address on userinfo.userid = address.userid where username = @username";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                user = new user(new usercred(Convert.ToInt32(rdr["idpassword"]), rdr["username"].ToString(), rdr["password"].ToString()),new userinfo(Convert.ToInt32(rdr["accounttype"]),new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())));
            }
            rdr.Close();
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
                studentinfo.Add(new student(new usercred(0, rdr["username"].ToString(), null),new userinfo(0,new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(),Convert.ToDateTime(rdr["dateofbirth"])),new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())), rdr["school"].ToString(), rdr["grade"].ToString(), rdr["cardid"].ToString()));
               
            }
            rdr.Close();
            return studentinfo;
        }
        public ArrayList getcourse()
        {
            db db = new db();
            ArrayList course= new ArrayList();
            SqlConnection conn = db.openconn();//add userid and password 
            String sql = "SELECT  course.courseid,department.department,coursenumber,coursename,description,firstname,middlename,lastname,suffix,credit FROM coursestudent join course on course.courseid = coursestudent.courseid join section on course.sectionid = section.sectionid join department on department.departmentid = section.department join userinfo on userinfo.nameid = course.teacherid where coursestudent.studentid = 1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                course.Add(new course(Convert.ToInt32(rdr["courseid"]), rdr["department"].ToString(), rdr["coursenumber"].ToString(), rdr["coursename"].ToString(), rdr["description"].ToString(), rdr["lastname"].ToString()));

            }
            rdr.Close();
            return course;
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
                staffinfo.Add(new staff(new usercred(0, rdr["username"].ToString(), null), new userinfo(0,new name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(), rdr["sex"].ToString(), Convert.ToDateTime(rdr["dateofbirth"])), new address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["zipcode"].ToString())), rdr["school"].ToString(), rdr["department"].ToString(), rdr["position"].ToString(),rdr["cardid"].ToString(), rdr["tenure"].ToString(), Convert.ToDateTime(rdr["hireddate"])));

            }
            rdr.Close();
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
            return hash;
        }

    }
}
