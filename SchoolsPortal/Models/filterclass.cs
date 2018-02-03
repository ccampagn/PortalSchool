using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class filterclass
    {
        private int districtid;
        private int schoolid;
        private int gradeid;
        private ArrayList courseid;
        private ArrayList sectionid;
        private ArrayList teacherid;

        public filterclass(int districtid,int schoolid,int gradeid,ArrayList courseid,ArrayList sectionid,ArrayList teacherid)
        {
            this.districtid = districtid;
            this.schoolid = schoolid;
            this.gradeid = gradeid;
            this.courseid = courseid;
            this.sectionid = sectionid;
            this.teacherid = teacherid;

        }
        public int getdistrict()
        {
            return districtid;
        }
        public int getschool()
        {
            return schoolid;
        }
        public int getgrade()
        {
            return gradeid;
        }
        public ArrayList getcourse()
        {
            return courseid;
        }
        public ArrayList getsection()
        {
            return sectionid;
        }
        public ArrayList getteacher()
        {
            return teacherid;
        }

    }
}