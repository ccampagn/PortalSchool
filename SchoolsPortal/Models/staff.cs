using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class staff : user
    {
        private usercred usercred;
        private userinfo userinfo;
        private string  school;
        private string department;
        private string position;
        private string cardid;
        private string tenure;
        private DateTime hiredate;
        public staff(usercred usercred, userinfo userinfo,string school,string department,string position,string cardid,string tenure,DateTime hiredate) : base(usercred, userinfo)
        {
            this.usercred = usercred;
            this.userinfo =userinfo;
            this.school = school;
            this.department = department;
            this.position = position;
            this.cardid = cardid;
            this.tenure = tenure;
            this.hiredate = hiredate;
        }
        public string getschool()
        {
            return school;
        }
        public string getdepartment()
        {
            return department;
        }
        public string getposition()
        {
            return department;
        }
        public string getcardid()
        {
            return cardid;
        }
        public string gettenure()
        {
            return department;
        }
        public DateTime gethiredate()
        {
            return hiredate;
        }
    }
}