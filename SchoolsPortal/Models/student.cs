using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class student : user
    {
        private usercred usercred;
        private userinfo userinfo;
        private string school;
        private string grade;
        private string cardid;

        public student(usercred usercred, userinfo userinfo,string school, string grade,string cardid) : base(usercred, userinfo)
        {
            this.usercred = usercred;
            this.userinfo = userinfo;
            this.school = school;
            this.grade = grade;
            this.cardid = cardid;
        }

        public string getschool()
        {
            return school;
        }
        public string getgrade()
        {
            return grade;
        }
        public string getcardid()
        {
            return cardid;
        }
    }
}