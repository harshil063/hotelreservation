using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hotelreservation.Models;

namespace hotelreservation.Controllers
{
    public class tbl_reservationController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        // GET: tbl_reservation
        public ActionResult Index()
        {
            var tbl_reservation = db.tbl_reservation.Include(t => t.tbl_customer).Include(t => t.tbl_room.tbl_roomtype);
            return View(tbl_reservation.ToList());
        }

        // GET: tbl_reservation/Details/5
        public ActionResult Details(int? id)
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

        // GET: tbl_reservation/Create
        public ActionResult Create()
        {
            //ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "fname");
            //ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "no_of_room");
            ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "email");
            ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "roomtype_id");
            return View();
        }

        // POST: tbl_reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "res_id,arrival_date,departure_date,no_of_adult,total_amount,booking_date,room_id,guest_id")] tbl_reservation tbl_reservation)
        {
            if (ModelState.IsValid)
            {
                db.tbl_reservation.Add(tbl_reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "fname", tbl_reservation.guest_id);
            ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "no_of_room", tbl_reservation.room_id);
            return View(tbl_reservation);
        }

        // GET: tbl_reservation/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "fname", tbl_reservation.guest_id);
            ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "no_of_room", tbl_reservation.room_id);
            return View(tbl_reservation);
        }

        // POST: tbl_reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "res_id,arrival_date,departure_date,no_of_adult,total_amount,booking_date,room_id,guest_id")] tbl_reservation tbl_reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guest_id = new SelectList(db.tbl_customer, "cust_id", "fname", tbl_reservation.guest_id);
            ViewBag.room_id = new SelectList(db.tbl_room, "room_id", "no_of_room", tbl_reservation.room_id);
            return View(tbl_reservation);
        }

        // GET: tbl_reservation/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: tbl_reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_reservation tbl_reservation = db.tbl_reservation.Find(id);
            db.tbl_reservation.Remove(tbl_reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
