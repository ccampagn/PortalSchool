using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class categorygrade
    {
        private int categorygradeid;
        private decimal gradepercent;
        private int categoryid;

        public categorygrade(int categorygradeid,decimal gradepercent,int categoryid)
        {
            this.categorygradeid = categorygradeid;
            this.gradepercent = gradepercent;
            this.categoryid = categoryid;
        }

        public int getcategorygradeid()
        {
            return categorygradeid;
        }

        public decimal getgradepercent()
        {
            return gradepercent;
        }

        public int getcategoryid()
        {
            return categoryid;
        }
    }
}