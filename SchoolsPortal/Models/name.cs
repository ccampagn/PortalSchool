using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class name
    {
        private int nameid;
        private string firstname;
        private string middlename;
        private string lastname;
        private string suffix;
        private string sex;
        private DateTime birthofdate;

        public name(int nameid, string firstname, string middlename, string lastname, string suffix, string sex, DateTime birthofdate)
        {
            this.nameid = nameid;
            this.firstname = firstname;
            this.middlename = middlename;
            this.lastname = lastname;
            this.suffix = suffix;
            this.sex = sex;
            this.birthofdate = birthofdate;
        }
        public string getfirstname()
        {
            return firstname;
        }
        public string getmiddlename()
        {
            return middlename;
        }
        public string getlastname()
        {
            return lastname;
        }
        public string getsuffix()
        {
            return suffix;
        }
        public string getsex()
        {
            return sex;
        }
        public DateTime getbirthofdate()
        {
            return birthofdate;
        }


    }
}