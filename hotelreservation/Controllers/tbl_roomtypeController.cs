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
    public class tbl_roomtypeController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        // GET: tbl_roomtype
        public ActionResult Index()
        {
            return View(db.tbl_roomtype.ToList());
        }

        // GET: tbl_roomtype/Details/5
        public ActionResult Details(int? id)
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

        // GET: tbl_roomtype/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_roomtype/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roomtype_id,room_type,room_price,room_photo,room_description")] tbl_roomtype tbl_roomtype)
        {
            if (ModelState.IsValid)
            {
                db.tbl_roomtype.Add(tbl_roomtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_roomtype);
        }

        // GET: tbl_roomtype/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "roomtype_id,room_type,room_price,room_photo,room_description")] tbl_roomtype tbl_roomtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_roomtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_roomtype);
        }

        // GET: tbl_roomtype/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: tbl_roomtype/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_roomtype tbl_roomtype = db.tbl_roomtype.Find(id);
            db.tbl_roomtype.Remove(tbl_roomtype);
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
