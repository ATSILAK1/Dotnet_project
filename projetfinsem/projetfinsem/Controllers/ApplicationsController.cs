using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projetfinsem.Models;

namespace projetfinsem.Controllers
{
    public class ApplicationsController : Controller
    {
        private projetfinsemEntities db = new projetfinsemEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var application = db.Application.Include(a => a.Categorie).Include(a => a.User);
            return View(application.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.idcategorie = new SelectList(db.Categorie, "idcat", "nom");
            ViewBag.idprop = new SelectList(db.User, "id", "nom");
            return View();
        }

        // POST: Applications/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nomapp,date_creation,idcategorie")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Application.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcategorie = new SelectList(db.Categorie, "idcat", "nom", application.idcategorie);
            ViewBag.idprop = new SelectList(db.User, "id", "nom", application.idprop);
            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategorie = new SelectList(db.Categorie, "idcat", "nom", application.idcategorie);
            ViewBag.idprop = new SelectList(db.User, "id", "nom", application.idprop);
            return View(application);
        }

        // POST: Applications/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idapp,idprop,nomapp,date_creation,nombre_telechargement,idcategorie")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcategorie = new SelectList(db.Categorie, "idcat", "nom", application.idcategorie);
            ViewBag.idprop = new SelectList(db.User, "id", "nom", application.idprop);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Application.Find(id);
            db.Application.Remove(application);
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
