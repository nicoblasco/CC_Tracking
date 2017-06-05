using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrackingCar.DAL;
using TrackingCar.Models;

namespace TrackingCar.Controllers
{
    public class CamerasController : Controller
    {
        private TrackingContext db = new TrackingContext();

        // GET: Cameras
        public ActionResult Index()
        {
            var cameras = db.Cameras.Include(c => c.City).Include(c => c.Country).Include(c => c.State);
            return View(cameras.ToList());
        }

        // GET: Cameras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = db.Cameras.Find(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            return View(camera);
        }

        // GET: Cameras/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "Descripcion");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Descripcion");
            ViewBag.StateID = new SelectList(db.States, "StateID", "Descripcion");
            return View();
        }

        // POST: Cameras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CameraID,Numero,Direccion,CountryID,StateID,CityID,MapCoordenadaX,MapCoordenadaY,Point,Limite")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                db.Cameras.Add(camera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "CityID", "Descripcion", camera.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Descripcion", camera.CountryID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Descripcion", camera.StateID);
            return View(camera);
        }

        // GET: Cameras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = db.Cameras.Find(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "Descripcion", camera.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Descripcion", camera.CountryID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Descripcion", camera.StateID);
            return View(camera);
        }

        // POST: Cameras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CameraID,Numero,Direccion,CountryID,StateID,CityID,MapCoordenadaX,MapCoordenadaY,Point,Limite")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(camera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "Descripcion", camera.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Descripcion", camera.CountryID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Descripcion", camera.StateID);
            return View(camera);
        }

        // GET: Cameras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = db.Cameras.Find(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            return View(camera);
        }

        // POST: Cameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Camera camera = db.Cameras.Find(id);
            db.Cameras.Remove(camera);
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
