using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class usercred
    {
        private int usercredid;
        private string username;
        private string password;
        
        public usercred(int usercredid,string username,string password)
        {
            this.usercredid = usercredid;
            this.username = username;
            this.password = password;

        }
        public int getuserid()
        {
            return usercredid;
        }
        public string getusername()
        {
            return username;
        }
        public string getpassword()
        {
            return password;
        }
    }
}