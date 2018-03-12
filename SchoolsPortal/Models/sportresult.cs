using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class sportresult
    {
        private int sportresultid;
        private DateTime date;
        private string oppname;
        private decimal yourscore;
        private decimal oppscore;
        public sportresult(int sportresultid,DateTime date,string oppname,decimal yourscore,decimal oppscore)
        {
            this.sportresultid = sportresultid;
            this.date = date;
            this.oppname = oppname;
            this.yourscore = yourscore;
            this.oppscore = oppscore;
        }
        public int getsportresultid()
        {
            return sportresultid;
        }
        public DateTime getdate()
        {
            return date;
        }
        public string getoppname()
        {
            return oppname;
        }
        public decimal getyourscore()
        {
            return yourscore;
        }
        public decimal getoppscore()
        {
            return oppscore;
        }

    }

}