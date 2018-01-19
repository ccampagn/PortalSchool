using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class messageboard
    {
        private int messageboardid;
        private name name;
        private DateTime datetime;
        private string message;

        public messageboard(int messageboardid,name name,DateTime datetime,string message) 
        {
            this.messageboardid = messageboardid;
            this.name = name;
            this.datetime = datetime;
            this.message = message;
        }

        public int getmessageboardid()
        {
            return messageboardid;
        }
        public name getname()
        {
            return name;
        }
        public DateTime getpostdate()
        {
            return datetime;
        }
        public string getmessage()
        {
            return message;
        }

    }
}