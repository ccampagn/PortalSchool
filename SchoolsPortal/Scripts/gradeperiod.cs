using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class gradeperiod
    {
        private int gradeperiodid;
        private string periodname;
        private DateTime periodstart;
        private DateTime periodend;
        private string letter;

        public gradeperiod(int gradeperiodid,string periodname,DateTime periodstart,DateTime periodend,string letter)
        {
            this.gradeperiodid = gradeperiodid;
            this.periodname = periodname;
            this.periodstart = periodstart;
            this.periodend = periodend;
            this.letter = letter;
        }
        public int getgradeperiodid()
        {
            return gradeperiodid;
        }
        public string getperiodname()
        {
            return periodname;
        }
        public DateTime getperiodstart()
        { 
            return periodstart;
        }
        public DateTime getperiodend()
        {
            return periodend;
        }
        public string getletter()
        {
            return letter;
        }
        public void setletter(string letter)
        {
            this.letter = letter;
        }

    }
}