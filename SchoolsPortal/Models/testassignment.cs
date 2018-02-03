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

        public testassignment(int testassignmentid,string assignmentname,ArrayList questions)
        {
            this.testassignmentid = testassignmentid;
            this.assignmentname = assignmentname;
            this.questions = questions;
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
    }
}