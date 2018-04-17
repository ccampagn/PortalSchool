using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class directory
    {
        private int nameid;
        private name name;
        private string grade;
        private string cardid;

        public directory(int nameid,name name,string grade, string cardid)
        {
            this.nameid = nameid;
            this.name = name;
            this.grade = grade;
            this.cardid = cardid;
        }

        public int getnameid()
        {
            return nameid;
        }
        public name getname()
        {
            return name;
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