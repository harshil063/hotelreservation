using hotelreservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;

namespace hotelreservation.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        HotelDBEntities1 db = new HotelDBEntities1();
        
        //[HttpGet]
        //// GET: Customer
        //public ActionResult SignUp()
        //{
        //    ViewBag.saveresult = "";
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SignUp(tbl_customer cst)
        //{
        //    using(HotelDBEntities1 entities = new HotelDBEntities1())
        //    {
        //        entities.tbl_customer.Add(cst);
        //        entities.SaveChanges();
        //    }
        //    return RedirectToAction("Login");
        //}

        //[HttpGet]
        //// GET: Admin
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //// GET: Admin
        //public ActionResult Login(tbl_customer cst)
        //{
            //    using(var context = new HotelDBEntities1())
            //    {
            //        bool isvalid = context.tbl_customer.Any(x => x.email == cst.email && x.password == cst.password);
            //        if (isvalid)
            //        {
            //            FormsAuthentication.SetAuthCookie(cst.email,false);
            //            return RedirectToAction("Home", "Customer");
            //        }
            //        ModelState.AddModelError("", "Invalid Username or Password");
            //        return View();
            //    }





//            tbl_customer cs = db.tbl_customer.Where(x => x.email == cst.email && x.password == cst.password).SingleOrDefault();
    //        if(cs != null)
    //        {
    //            Session["cust_id"] = cs.cust_id.ToString();
    //            return RedirectToAction("home");
    //        }
    //        else
    //        {
    //            ViewBag.err = "Invalid user name or password";
    //        }
    //        return View();
    //    }


    public ActionResult Home()
        {
            return View();
        }

        
        public ActionResult view_roomtype()
        {
            return View(db.tbl_roomtype.ToList());
        }

        
        public ActionResult details_roomtype(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_roomtype tbl_roomtype = db.tbl_roomtype.Find(id);
            if (tbl_roomtype == null)
            {
                return HttpNotFound();
            }
            return View(tbl_roomtype);
        }


        //=========this all for view room to customer from tbl_room table==================================================================================
        
        public async Task<ActionResult> cust_viewroom(string searchString)
        {
            var rooms = from r in db.tbl_room select r;
            if (!String.IsNullOrEmpty(searchString))
            {
               rooms = rooms.Where(s => s.tbl_roomtype.room_type.Contains(searchString));
                
              ViewBag.emptyerr = "Not found Any Room..!";
                
            }
            
            //return View(db.tbl_room.ToList());
            return View(await rooms.ToListAsync());
        }


        public ActionResult cust_detailsroom(int? id,string roomtypeid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_room tbl_room = db.tbl_room.Find(id);
            if (tbl_room == null)
            {
                return HttpNotFound();
            }
            if (!String.IsNullOrEmpty(roomtypeid))
            {
                
                var avroom = db.tbl_room.Find(id);
                if (int.Parse(avroom.no_of_room) > 0)
                {
                    ViewBag.avst = "Room is available";
                }
                else
                {
                    ViewBag.avst = "Room not available";
                }
            }
            
            //ViewData["room_id"] = new SelectList(db.tbl_room, "room_id", "no_of_room");

            return View(tbl_room);
        }

        // GET: Movies
        //public async Task<ActionResult> cust_detailsroom(int? id,string roomtype)
        //{
        //    // Use LINQ to get list of genres.
        //    IQueryable<string> genreQuery = from m in db.tbl_room select m.tbl_roomtype.room_type;
        //    var movies = from m in db.tbl_room select m;


        //    if (!string.IsNullOrEmpty(roomtype))
        //    {
        //        movies = movies.Where(x => x.tbl_roomtype.room_type == roomtype);
        //    }

        //    var movieGenreVM = new tbl_room
        //    {
        //        room_type = new SelectList(await genreQuery.Distinct().ToListAsync()),
                
        //    };

        //    return View(movieGenreVM);
        //}


        //==================for reserve a room in tbl_reservation table
        // GET: tbl_reservation
        public ActionResult cust_viewreservation(int? id)
        {
            var s_custid = Session["cust_id"];
            return View(db.tbl_reservation.Where(x => x.guest_id == id).ToList());


            //using (var ctx = new HotelDBEntities1())
            //{
            //    ctx.Configuration.LazyLoadingEnabled = false;

            //    var res = ctx.tbl_reservation.SqlQuery("select * from tbl_reservation where guest_id= 34").ToList();
            //    return View(res);
            //}
        }

        [HttpGet]
        // GET: Customer
        public ActionResult cust_reservation(int? id, int? price)
        {
            ViewBag.ID = id;
            ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "email",selectedValue:Session["cust_id"]);
            ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "room_id",selectedValue:id);
            Session["room_price"] = price.ToString();

            Session["booking_date"] = DateTime.Now;


            tbl_room tbl_room = db.tbl_room.Find(id);
            var noofroom = tbl_room.no_of_room;
            var room_type = tbl_room.tbl_roomtype.room_type;
            var room_photo = tbl_room.tbl_roomtype.room_photo;
            var room_desc = tbl_room.tbl_roomtype.room_description;
            //var noofroom = int.Parse(tbl_room.no_of_room);
            db.tbl_room.Attach(tbl_room);
            tbl_room.no_of_room = (int.Parse(noofroom) - 1).ToString();

            db.Entry(tbl_room).Property(x => x.no_of_room).IsModified = true;
            db.SaveChanges();


            Session["room_type"] = room_type.ToString();
            Session["room_photo"] = room_photo.ToString();
            Session["room_desc"] = room_desc.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cust_reservation([Bind(Include = "res_id,fname,lname,email,arrival_date,departure_date,no_of_adult,total_amount,booking_date,room_id,guest_id")] tbl_reservation tbl_reservation)
        {
            if (ModelState.IsValid)
            {
                db.tbl_reservation.Add(tbl_reservation);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("cust_payment");
                
            }
            

            Session["t_amount"] = ViewData["total_amount"].ToString();
            ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "fname", tbl_reservation.guest_id);
            ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "no_of_room", tbl_reservation.room_id);
            return View(tbl_reservation);
        }

        public ActionResult cust_detailsres(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_reservation tbl_reservation = db.tbl_reservation.Find(id);
            if (tbl_reservation == null)
            {
                return HttpNotFound();
            }
            return View(tbl_reservation);
        }

        
        public ActionResult cust_payment()
        {
            //ViewBag.amount = Content(HttpContext.Request.Form["total_amount"]);
            return View();
        }


       




    }
}