using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class userinfo
    {
        private int usertype;
        private name name;
        private address address;


        public userinfo(int usertype,name name, address address)
        {
            this.usertype = usertype;
            this.name = name;
            this.address = address;
        }

        public int getusertype()
        {
            return usertype;
         }
        public name getname()
        {
            return name;
        }
        public address getaddress()
        {
            return address;
        }
    }
}