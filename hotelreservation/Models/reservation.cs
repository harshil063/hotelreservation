using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hotelreservation.Models
{
    public class reservation
    {
        [Key]
        public int res_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public DateTime arrival_date { get; set; }
        public DateTime departure_date { get; set; }
        public int no_of_adult { get; set; }
        public int total_amount { get; set; }
        public DateTime booking_date { get; set; }

        //[Display(Name ="tbl_room")]
        //public int room_id { get; set; }

        //[ForeignKey("room_id")]
        //public virtual tbl_room room_type { get; set; }
    
        //[Display(Name ="tbl_customer")]
        //public int cust_id { get; set; }

        //[ForeignKey("customer_id")]
        //public virtual tbl_customer email { get; set; }

    }
}