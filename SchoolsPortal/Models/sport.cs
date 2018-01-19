using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class sport
    {
        private int sportid;
        private string seasonname;
        private string sex;
        private string sportname;
        private string levelname;
        private name coach;
        public sport(int sportid, string seasonname, string sex, string sportname, string levelname, name coach)
        {
            this.sportid = sportid;
            this.seasonname = seasonname;
            this.sex = sex;
            this.sportname = sportname;
            this.levelname = levelname;
            this.coach = coach;
        }
        public int getsportid()
        {
            return sportid;
        }
        public string getseasonname()
        {
            return seasonname;
        }
        public string getsex()
        {
            return sex;
        }
        public string getsportname()
        {
            return sportname;
        }
        public string getlevelname()
        {
            return levelname;
        }
        public name getcoach()
        {
            return  coach;
        }
    }
}