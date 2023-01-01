using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Bibliotheque_appController : Controller
    {
        private profinsemEntities2 db = new profinsemEntities2();

        // GET: Bibliotheque_app
        public ActionResult Index()
        {
            var bibliotheque_app = db.Bibliotheque_app.Include(b => b.bibliotheque).Include(b => b.u_application);
            return View(bibliotheque_app.ToList());
        }

        // GET: Bibliotheque_app/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibliotheque_app bibliotheque_app = db.Bibliotheque_app.Find(id);
            if (bibliotheque_app == null)
            {
                return HttpNotFound();
            }
            return View(bibliotheque_app);
        }

        // GET: Bibliotheque_app/Create
        public ActionResult Create()
        {
            ViewBag.id_bib = new SelectList(db.bibliotheque, "id_bib", "id_bib");
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app");
            return View();
        }

        // POST: Bibliotheque_app/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_app,id_bib,date_telechargement")] Bibliotheque_app bibliotheque_app)
        {
            if (ModelState.IsValid)
            {
                db.Bibliotheque_app.Add(bibliotheque_app);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bib = new SelectList(db.bibliotheque, "id_bib", "id_bib", bibliotheque_app.id_bib);
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app", bibliotheque_app.id_app);
            return View(bibliotheque_app);
        }

        // GET: Bibliotheque_app/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibliotheque_app bibliotheque_app = db.Bibliotheque_app.Find(id);
            if (bibliotheque_app == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bib = new SelectList(db.bibliotheque, "id_bib", "id_bib", bibliotheque_app.id_bib);
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app", bibliotheque_app.id_app);
            return View(bibliotheque_app);
        }

        // POST: Bibliotheque_app/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_app,id_bib,date_telechargement")] Bibliotheque_app bibliotheque_app)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bibliotheque_app).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bib = new SelectList(db.bibliotheque, "id_bib", "id_bib", bibliotheque_app.id_bib);
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app", bibliotheque_app.id_app);
            return View(bibliotheque_app);
        }

        // GET: Bibliotheque_app/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibliotheque_app bibliotheque_app = db.Bibliotheque_app.Find(id);
            if (bibliotheque_app == null)
            {
                return HttpNotFound();
            }
            return View(bibliotheque_app);
        }

        // POST: Bibliotheque_app/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bibliotheque_app bibliotheque_app = db.Bibliotheque_app.Find(id);
            db.Bibliotheque_app.Remove(bibliotheque_app);
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
