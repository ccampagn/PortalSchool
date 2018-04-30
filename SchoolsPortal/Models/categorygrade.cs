using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class categorygrade
    {
        private int categorygradeid;//categorygrade
        private decimal gradepercent;//gradepercent
        private int categoryid;//categoryid

        public categorygrade(int categorygradeid,decimal gradepercent,int categoryid)//construction
        {
            this.categorygradeid = categorygradeid;
            this.gradepercent = gradepercent;
            this.categoryid = categoryid;
        }

        public int getcategorygradeid()//get category grade id
        {
            return categorygradeid;
        }

        public decimal getgradepercent()//getgradepercent
        {
            return gradepercent;
        }

        public int getcategoryid()//getcategoryid
        {
            return categoryid;
        }
    }
}