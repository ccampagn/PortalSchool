using System;

namespace SchoolsPortal.Models
{
    public class assignment
    {
        private int assignmentid;
        private string title;
        private string gradingperiod;
        private DateTime postdate;
        private DateTime duedate;
        private decimal scores;
        private decimal points;
        private string category;
        private int testsid;
        private int teststatus;

        public assignment(int assignmentid, string title, string gradingperiod, DateTime postdate, DateTime duedate, decimal scores, decimal points, string category,int testsid,int teststatus)
        {
            this.assignmentid = assignmentid;
            this.title = title;
            this.gradingperiod = gradingperiod;
            this.postdate = postdate;
            this.duedate = duedate;
            this.scores = scores;
            this.points = points;
            this.category = category;
            this.testsid = testsid;
            this.teststatus = teststatus;
        }

        public int getassignment()
        {
            return assignmentid;
        }
        public string gettitle()
        {
            return title;
        }
        public string getgradingperiod()
        {
            return gradingperiod;
        }
        public DateTime getpostdate()
        {
            return postdate;
        }
        public DateTime getduedate()
        {
            return duedate;
        }
        public decimal getscores()
        {
            return scores;
        }
        public decimal getpoints()
        {
            return points;
        }
        public string getcategory()
        {
            return category;
        }
        public int gettestsid()
        {
            return testsid;
        }
        public int getteststatus()
        {
            return teststatus;
        }
    }
}