using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class newstories
    {
        private int newstoriesid;
        private DateTime posttime;
        private string title;
        private string body;
        private name author;

        public newstories(int newstoriesid,DateTime posttime,string title, string body,name author)
        {
            this.newstoriesid = newstoriesid;
            this.posttime = posttime;
            this.title = title;
            this.body = body;
            this.author = author;
        }

        public int getnewstoriesid()
        {
            return newstoriesid;
        }
        public DateTime getposttime()
        {
            return posttime;
        }
        public string gettitle()
        {
            return title;
        }
        public string getbody()
        {
            return body;
        }
        public name getauthor()
        {
            return author;
        }
    }
}