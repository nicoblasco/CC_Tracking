using PagedList;
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
    public class TrackingsController : Controller
    {
        private TrackingContext db = new TrackingContext();

        // GET: Trackings
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }

            var trackings = db.Trackings.Include(t => t.Camera);
            return View(trackings.ToList());
        }
        // GET: Trackings/Estadisticas
        //public ActionResult Estadisticas(int? page)
        //{
        //    if (!Request.IsAuthenticated)
        //    {
        //        return RedirectToAction("LogIn", "Account");
        //    }

        //    var trackings = db.Trackings.Include(t => t.Camera);
        //    return View(trackings.ToList());
        //}

        public ViewResult Estadisticas(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var trackings = from s in db.Trackings
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                trackings = trackings.Where(s => s.Patente.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    trackings = trackings.OrderByDescending(s => s.Patente);
                    break;
                case "Date":
                    trackings = trackings.OrderBy(s => s.FechaHora);
                    break;
                case "date_desc":
                    trackings = trackings.OrderByDescending(s => s.FechaHora);
                    break;
                default:  // ID ascending 
                    trackings = trackings.OrderBy(s => s.ID);
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(trackings.ToPagedList(pageNumber, pageSize));
        }

        // GET: Trackings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // GET: Trackings/Create
        public ActionResult Create()
        {
            ViewBag.CameraID = new SelectList(db.Cameras, "CameraID", "Numero");
            return View();
        }

        // POST: Trackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FechaHora,CameraID,Velocidad,Patente,FotoUrl")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                db.Trackings.Add(tracking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CameraID = new SelectList(db.Cameras, "CameraID", "Numero", tracking.CameraID);
            return View(tracking);
        }

        // GET: Trackings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CameraID = new SelectList(db.Cameras, "CameraID", "Numero", tracking.CameraID);
            return View(tracking);
        }

        // POST: Trackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FechaHora,CameraID,Velocidad,Patente,FotoUrl")] Tracking tracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tracking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CameraID = new SelectList(db.Cameras, "CameraID", "Numero", tracking.CameraID);
            return View(tracking);
        }

        // GET: Trackings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracking tracking = db.Trackings.Find(id);
            if (tracking == null)
            {
                return HttpNotFound();
            }
            return View(tracking);
        }

        // POST: Trackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tracking tracking = db.Trackings.Find(id);
            db.Trackings.Remove(tracking);
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
