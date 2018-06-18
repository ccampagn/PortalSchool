using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class assignmentid
    {
        private name name;
        private decimal scores;
        private decimal points;
      

        public assignmentid(name name, decimal scores,decimal points)
        {
            this.name = name;
            this.scores = scores;
            this.points = points;
        }

        public name getname()
        {
            return name;
        }
        public decimal getscores()
        {
            return scores;
        }
        public decimal getpoints()
        {
            return points;
        }
    }
}