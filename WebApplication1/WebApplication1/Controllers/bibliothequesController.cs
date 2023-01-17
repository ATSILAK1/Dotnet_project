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
    public class bibliothequesController : Controller
    {
        private profinsemEntities db = new profinsemEntities();

        // GET: bibliotheques
        public ActionResult Index()
        {
            try
            {
                var user = Session["user"] as utilisateur;
                var bib = db.bibliotheque.Find(user.bibliotheque_id_bib);
                     return View(bib.Bibliotheque_app.ToList());
            }
            catch(System.NullReferenceException )
            {
                return RedirectToAction("Login", "utilisateur");
            }
               
        }

        // GET: bibliotheques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bibliotheque bibliotheque = db.bibliotheque.Find(id);
            if (bibliotheque == null)
            {
                return HttpNotFound();
            }
            return View(bibliotheque);
        }

        // GET: bibliotheques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: bibliotheques/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_bib,id_user")] bibliotheque bibliotheque)
        {

            if (ModelState.IsValid)
            {
               
                db.bibliotheque.Add(bibliotheque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bibliotheque);
        }

        // GET: bibliotheques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bibliotheque bibliotheque = db.bibliotheque.Find(id);
            if (bibliotheque == null)
            {
                return HttpNotFound();
            }
            return View(bibliotheque);
        }

        // POST: bibliotheques/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_bib,id_user")] bibliotheque bibliotheque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bibliotheque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bibliotheque);
        }

        // GET: bibliotheques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bibliotheque bibliotheque = db.bibliotheque.Find(id);
            if (bibliotheque == null)
            {
                return HttpNotFound();
            }
            return View(bibliotheque);
        }

        // POST: bibliotheques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bibliotheque bibliotheque = db.bibliotheque.Find(id);
            db.bibliotheque.Remove(bibliotheque);
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
