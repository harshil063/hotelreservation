using hotelreservation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace hotelreservation.Controllers
{
    public class AdminController : Controller
    {
        HotelDBEntities1 db = new HotelDBEntities1();
        
        [HttpGet]
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_admin adm)
        {
            tbl_admin ad = db.tbl_admin.Where(x => x.ad_name == adm.ad_name && x.ad_password == adm.ad_password).SingleOrDefault();
            if(ad != null)
            {
                Session["ad_id"] = ad.ad_id.ToString();
                return RedirectToAction("Admindashboard");
            }
            else
            {
                ViewBag.err = "Invalid username or password!";
            }
            return View();
        }




        public ActionResult Admindashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult addroomtype()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addroomtype([Bind(Include = "roomtype_id,room_type,room_price,room_photo,room_description")] tbl_roomtype rt)
        {
            db.tbl_roomtype.Add(rt);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.saveresult = "Added successfully";
            

            return RedirectToAction("/viewroomtype");
        }

        [HttpGet]
        public ActionResult viewroomtype()
        {
            return View(db.tbl_roomtype.ToList());
        }


        [HttpPost]
        public ActionResult viewroomtype(tbl_roomtype rt)
        {
            return View();
        }


        public ActionResult deleteroomtype(int? id)
        {
            if(id == null)
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

        // POST: tbl_roomtype/Delete/5
        [HttpPost, ActionName("deleteroomtype")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_roomtype tbl_roomtype = db.tbl_roomtype.Find(id);
            db.tbl_roomtype.Remove(tbl_roomtype);
            db.SaveChanges();
            ViewBag.delresult = "Deleted successfully";
            return RedirectToAction("/viewroomtype");

        }


        // GET: tbl_roomtype/Edit/5
        public ActionResult editroomtype(int? id)
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


        // POST: tbl_roomtype/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editroomtype([Bind(Include = "roomtype_id,room_type,room_price,room_photo,room_description")] tbl_roomtype tbl_roomtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_roomtype).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.saveresult = "Updated successfully..!";
                return RedirectToAction("/viewroomtype");
            }
            return View("viewroomtype");
        }



        // GET: tbl_roomtype/Details/5
        public ActionResult detailsroomtype(int? id)
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

  //==============================================================================
        // GET: tbl_room
        public ActionResult viewroom()
        {
            var tbl_room = db.tbl_room.Include(t => t.tbl_roomtype);
            return View(tbl_room.ToList());
        }

        // GET: tbl_room/Details/5
        public ActionResult detailsroom(int? id)
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
            return View(tbl_room);
        }


        // GET: tbl_room/Create
        public ActionResult addroom()
        {
            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type");
            return View();
        }


        // POST: tbl_room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addroom([Bind(Include = "room_id,no_of_room,roomtype_id")] tbl_room tbl_room)
        {
            if (ModelState.IsValid)
            {
                db.tbl_room.Add(tbl_room);
                db.SaveChanges();
                ViewBag.saveresult = "Added successfully";
                return RedirectToAction("/Viewroom");
            }
            
            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type", tbl_room.roomtype_id);
            return View(tbl_room);
        }


        // GET: tbl_room/Edit/5
        public ActionResult editroom(int? id)
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
            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type", tbl_room.roomtype_id);
            return View(tbl_room);
        }


        // POST: tbl_room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editroom([Bind(Include = "room_id,no_of_room,roomtype_id")] tbl_room tbl_room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_room).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.saveresult = "Updated successfully";
                return RedirectToAction("viewroom");
            }
            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type", tbl_room.roomtype_id);
            return View(tbl_room);
        }


        //// GET: tbl_room/Delete/5
        public ActionResult deleteroom(int? id)
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
            return View(tbl_room);
        }

        // POST: tbl_room/Delete/5
        [HttpPost, ActionName("deleteroom")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteroomConfirmed(int id)
        {
            tbl_room tbl_room = db.tbl_room.Find(id);
            db.tbl_room.Remove(tbl_room);
            db.SaveChanges();
            ViewBag.delresult = "Deleted successfully..!";
            return RedirectToAction("viewroom");
        }
        //=============================================================================
        public ActionResult viewreservations()
        {
            var tbl_reservation = db.tbl_reservation.Include(t => t.tbl_customer).Include(t => t.tbl_room.tbl_roomtype);
            return View(tbl_reservation.ToList());
        }

        public ActionResult detailsres(int? id)
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


        public ActionResult viewres()
        {
            using (var ctx = new HotelDBEntities1())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                var res = ctx.tbl_reservation.SqlQuery("select * from tbl_reservation").ToList();
                //var query = "select * from tbl_reservation where res_id = 53";
                return View(res);
            }


        }

        public ActionResult viewusers()
        {
            var tbl_customer = db.tbl_customer;
            return View(tbl_customer.ToList());
        }

    }
}