using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class events
    {
        private int eventid;
        private string eventname;
        private string eventdescription;
        private DateTime eventdate;

        public events(int eventid,string eventname,string eventdescription,DateTime eventdate)
        {
            this.eventid = eventid;
            this.eventname = eventname;
            this.eventdescription = eventdescription;
            this.eventdate = eventdate;
        }
        public int geteventid()
        {
            return eventid;
        }
        public string geteventname()
        {
            return eventname;
        }
        public string geteventdescription()
        {
            return eventdescription;
        }
        public DateTime geteventdate()
        {
            return eventdate;
        }
    }
}