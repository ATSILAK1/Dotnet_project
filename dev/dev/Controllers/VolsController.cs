using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using dev.Models;

namespace dev.Controllers
{
    public class VolsController : Controller
    {
        private BD_aeroportEntities db = new BD_aeroportEntities();

        // GET: Vols
        public ActionResult Index()
        {
            //var pquery = db.Vol.Where(elt => elt.date_depart > new DateTime(2017, 9, 1)  && elt.date_depart < new DateTime(2017, 9, 30) );
             //var vol = db.Vol.Include(v => v.Avion).Include(v => v.Pilote);
            //var pquery = vol.Where(v => v.date_depart > new DateTime(2017, 9,1) && v.date_depart < new DateTime(2017,9,30));

            var vol = db.Vol.Include(v => v.Pilote);
           var pquery = vol.Where(v => v.ville_depart == "rabat" && (v.ville_arrive == "Paris" || v.ville_arrive == "Madrid"));

            ViewData["vol"] = db.Vol.Where(c => c.ville_depart == "Rabat" && c.Avion.capacite < 300).Count();
           
            return View(pquery.ToList());
        }

        // GET: Vols/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // GET: Vols/Create
        public ActionResult Create()
        {
            ViewBag.num_av = new SelectList(db.Avion, "num_av", "nom_av");
            ViewBag.num_pil = new SelectList(db.Pilote, "num_pil", "nom_pil");
            return View();
        }

        // POST: Vols/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "num_vol,num_pil,num_av,ville_depart,ville_arrive,date_depart,date_arrivee")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                db.Vol.Add(vol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.num_av = new SelectList(db.Avion, "num_av", "nom_av", vol.num_av);
            ViewBag.num_pil = new SelectList(db.Pilote, "num_pil", "nom_pil", vol.num_pil);
            return View(vol);
        }

        // GET: Vols/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            ViewBag.num_av = new SelectList(db.Avion, "num_av", "nom_av", vol.num_av);
            ViewBag.num_pil = new SelectList(db.Pilote, "num_pil", "nom_pil", vol.num_pil);
            return View(vol);
        }

        // POST: Vols/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "num_vol,num_pil,num_av,ville_depart,ville_arrive,date_depart,date_arrivee")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.num_av = new SelectList(db.Avion, "num_av", "nom_av", vol.num_av);
            ViewBag.num_pil = new SelectList(db.Pilote, "num_pil", "nom_pil", vol.num_pil);
            return View(vol);
        }

        // GET: Vols/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vol vol = db.Vol.Find(id);
            db.Vol.Remove(vol);
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
