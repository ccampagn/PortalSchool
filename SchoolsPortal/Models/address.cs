using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class address
    {
        private int addressid;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string zipcode;
        public address(int addressid,string address1, string address2, string city, string state, string zipcode)
        {
            this.addressid = addressid;
            this.address1 = address1;
            this.address2 = address2;
            this.city = city;
            this.state = state;
            this.zipcode = zipcode;
        }

        public int getaddressid()
        {
            return addressid;
        }
        public string getaddress1()
        {
            return address1;
        }
        public string getaddress2()
        {
            return address2;
        }
        public string getcity()
        {
            return city;
        }
        public string getstate()
        {
            return state;
        }
        public string getzipcode()
        {
            return zipcode;
        }
    }
}