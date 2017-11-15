using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class user
    {
        private usercred usercred;
        private userinfo userinfo;

        public user(usercred usercred,userinfo userinfo)
        {
            this.usercred = usercred;
            this.userinfo = userinfo;
        }

        public  usercred getusercred()
        {
            return usercred;
        }
        public userinfo getuserinfo()
        {
            return userinfo;
        }

    }
}