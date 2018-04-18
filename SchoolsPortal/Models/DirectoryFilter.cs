using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolsPortal.Models
{
    public class DirectoryFilter
    {
        private ArrayList position =new ArrayList();
        private DirectoryFilter(ArrayList position)
        {
            this.position = position;
        }

        public ArrayList getposition()
        {
            return position;
        }
    }
}