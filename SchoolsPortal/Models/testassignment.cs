using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class testassignment
    {
        int testassignmentid;
        string assignmentname;
        ArrayList questions;
        int testlimit;

        public testassignment(int testassignmentid,string assignmentname,ArrayList questions,int testlimit)
        {
            this.testassignmentid = testassignmentid;
            this.assignmentname = assignmentname;
            this.questions = questions;
            this.testlimit = testlimit;
        }

        public int gettestassignmentid()
        {
            return testassignmentid;
        }

        public string getassignmentname()
        {
            return assignmentname;
        }

        public ArrayList getquestions()
        {
            return questions;
        }

        public int gettestlimit()
        {
            return testlimit;
        }
    }
}