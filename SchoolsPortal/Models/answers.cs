using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class answers
    {
        private int answersid;
        private string answerstext;

        public answers(int answersid,string answerstext)
        {
            this.answersid = answersid;
            this.answerstext = answerstext;
        }

        public int getanswersid()
        {
            return answersid;
        }
        public string getanswerstext()
        {
            return answerstext;
        }
    }
}