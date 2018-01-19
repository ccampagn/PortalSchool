using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class schoolyear
    {
        private int schoolyearid;
        private string schoolyearname;
        private bool select;
            public schoolyear(int schoolyearid, string schoolyearname,bool select)
        {
            this.schoolyearid = schoolyearid;
            this.schoolyearname = schoolyearname;
            this.select = select;
        }

        public int getschoolyearid()
        {
            return schoolyearid;
        }
        public string getschoolyearname()
        {
            return schoolyearname;
        }
        public bool getselect()
        {
            return select;
        }
    }
}