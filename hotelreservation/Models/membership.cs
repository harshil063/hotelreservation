using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotelreservation.Models
{
    public class membership
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string city { get; set; }
        public int pincode { get; set; }
        public Int32 contact_no { get; set; }
        public string email { get; set; }
        public Int32 aadharcard_no { get; set; }
        public string password { get; set; }
        public string cpassword { get; set; }
    }
}