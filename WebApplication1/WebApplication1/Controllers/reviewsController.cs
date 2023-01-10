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
    public class reviewsController : Controller
    {
        private profinsemEntities db = new profinsemEntities();

        // GET: reviews
        public ActionResult Index()
        {
            var review = db.review.Include(r => r.u_application).Include(r => r.utilisateur);
            return View(review.ToList());
        }

        // GET: reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review review = db.review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: reviews/Create
        public ActionResult Create()
        {
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app");
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur");
            return View();
        }

        // POST: reviews/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_review,Commentaire,id_app,id_user")] review review , int id)
        {
            string commentaire = Request.Form["commentaire"];
            review.Commentaire = commentaire;
            review.utilisateur = db.utilisateur.Find((int)Session["user_id"]);
            review.u_application = db.u_application.Find(id);


            if (ModelState.IsValid)
            {
                db.review.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details","u_application",new {id=id});
            }

            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app", review.id_app);
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur", review.id_user);
            return View(review);
        }

        // GET: reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review review = db.review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app", review.id_app);
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur", review.id_user);
            return View(review);
        }

        // POST: reviews/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_review,Commentaire,id_app,id_user")] review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_app = new SelectList(db.u_application, "id_app", "nom_app", review.id_app);
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur", review.id_user);
            return View(review);
        }

        // GET: reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review review = db.review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            review review = db.review.Find(id);
            db.review.Remove(review);
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
