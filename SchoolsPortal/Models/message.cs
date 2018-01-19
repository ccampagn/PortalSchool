using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class message
    {
        private int messageid;
        private string messagetitle;
        private string messagetext;
        private DateTime senddate;
        private name from;
        public message(int messageid, string messagetitle, string messagetext, DateTime senddate,name from){
            this.messageid = messageid;
            this.messagetitle = messagetitle;
            this.messagetext = messagetext;
            this.senddate = senddate;
            this.from = from;
            }

        public int getmessageid()
        {
            return messageid;
        }
        public string getmessagetitle()
        {
            return messagetitle;
        }
        public string getmessagetext()
        {
            return messagetext;
        }
        public DateTime getsenddate()
        {
            return senddate;
        }
        public name getfrom()
        {
            return from;
        }
    }
}