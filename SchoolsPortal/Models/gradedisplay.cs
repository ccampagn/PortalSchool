using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class gradedisplay
    {
        private int gradedisplayid;
        private string categorytitle;
        private int type;
        private decimal percent;
        private decimal periodpercent;

        public gradedisplay(int gradedisplayid, string categorytitle,int type, decimal percent,decimal periodpercent)
        {
            this.gradedisplayid = gradedisplayid;
            this.categorytitle = categorytitle;
            this.type = type;
            this.percent = percent;
            this.periodpercent = periodpercent;
        }

        public int getgradedisplayid()
        {
            return gradedisplayid;
        }
        public string getcategorytitle()
        {
            return categorytitle;
        }
        public int gettype()
        {
            return type;
        }
        public decimal getpercent()
        {
            return percent;
        }
        public decimal getperiodpercent()
        {
            return periodpercent;
        }
        public void setpercent(decimal percent)
        {
            this.percent=percent;
        }
    }
}