using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class position
    {
        private int positionid;
        private string positionname;
        public position(int positionid, string positionname)
        {
            this.positionid = positionid;
            this.positionname = positionname;
        }
        public int getpositionid()
        {
            return positionid;
        }
        public string getpositionname()
        {
            return positionname;
        }
    }
}