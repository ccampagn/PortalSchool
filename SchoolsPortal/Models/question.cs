using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class question
    {
        private int questionid;
        private string questiontext;
        private ArrayList answers;
        private int correctanswer;
        private decimal points;
        public question(int questionid,string questiontext,ArrayList answers,int correctanswer,decimal points)
        {
            this.questionid = questionid;
            this.questiontext = questiontext;
            this.answers = answers;
            this.correctanswer = correctanswer;
            this.points = points;
        }
        public int getquestionid()
        {
            return questionid;
        }
        public string getquestiontext()
        {
            return questiontext;
        }
        public ArrayList getanswers()
        {
            return answers;
        }
        public int getcorrectanswer()
        {
            return correctanswer;
        }
        public decimal getpoints()
        {
            return points;
        }
    }
}