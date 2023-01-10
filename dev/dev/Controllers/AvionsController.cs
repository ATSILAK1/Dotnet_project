using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dev.Models;

namespace dev.Controllers
{
    public class AvionsController : Controller
    {
        private BD_aeroportEntities db = new BD_aeroportEntities();

        // GET: Avions
        public ActionResult Index()
        {


         
            
            
            
            
            return View(db.Avion.ToList());
        }


        public ActionResult Trier()
        {
            var pquery = from av in db.Avion orderby av.capacite select av;
            return View(pquery.ToList());
        }


        // GET: Avions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = db.Avion.Find(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // GET: Avions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avions/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "num_av,nom_av,capacite,localisation")] Avion avion)
        {
            if (ModelState.IsValid)
            {
                db.Avion.Add(avion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avion);
        }

        // GET: Avions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = db.Avion.Find(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // POST: Avions/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "num_av,nom_av,capacite,localisation")] Avion avion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(avion);
        }

        // GET: Avions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = db.Avion.Find(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // POST: Avions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avion avion = db.Avion.Find(id);
            db.Avion.Remove(avion);
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
