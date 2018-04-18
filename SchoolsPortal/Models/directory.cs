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
        private string gradedept;
        private string cardid;
        private string position;

        public directory(int nameid,name name,string gradedept, string cardid,string position)
        {
            this.nameid = nameid;
            this.name = name;
            this.gradedept = gradedept;
            this.cardid = cardid;
            this.position = position;
        }

        public int getnameid()
        {
            return nameid;
        }
        public name getname()
        {
            return name;
        }
        public string getgradedept()
        {
            return gradedept;
        }
        public string getcardid()
        {
            return cardid;
        }
        public string getposition()
        {
            return position;
        }



    }
}