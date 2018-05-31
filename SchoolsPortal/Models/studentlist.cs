using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class studentlist
    {
        private name name;
        private decimal grade;
        public studentlist(name name,decimal grade)
        {
            this.name = name;
            this.grade = grade;
        }

        public name getname()
        {
            return name;
        }
        public decimal getgrade()
        {
            return grade;
        }       
        public void setgrade(decimal grade)
        {
            this.grade = grade;
        }
    }
}