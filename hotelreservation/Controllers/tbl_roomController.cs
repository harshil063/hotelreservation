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
    public class tbl_roomController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        // GET: tbl_room
        public ActionResult Index()
        {
            var tbl_room = db.tbl_room.Include(t => t.tbl_roomtype);
            return View(tbl_room.ToList());
        }

        // GET: tbl_room/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {
            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type");
            return View();
        }

        // POST: tbl_room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "room_id,no_of_room,roomtype_id")] tbl_room tbl_room)
        {
            if (ModelState.IsValid)
            {
                db.tbl_room.Add(tbl_room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type", tbl_room.roomtype_id);
            return View(tbl_room);
        }

        // GET: tbl_room/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "room_id,no_of_room,roomtype_id")] tbl_room tbl_room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roomtype_id = new SelectList(db.tbl_roomtype, "roomtype_id", "room_type", tbl_room.roomtype_id);
            return View(tbl_room);
        }

        // GET: tbl_room/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_room tbl_room = db.tbl_room.Find(id);
            db.tbl_room.Remove(tbl_room);
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
