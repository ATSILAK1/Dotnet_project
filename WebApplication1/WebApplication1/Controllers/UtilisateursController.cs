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
    public class UtilisateursController : Controller
    {
        private profinsemEntities2 db = new profinsemEntities2();

        // GET: utilisateurs
        public ActionResult Index()
        {
            if ((string)Session["role"] != "Admin")
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "vous avez pas les droits");

            return View(db.utilisateur.ToList());
        }

        // GET: utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: utilisateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: utilisateurs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_user,nom_utilisateur,email,nom,prenom,date_naissance,motdepasse")] utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                utilisateur.u_role = "user";
                db.utilisateur.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utilisateur);
        }

        // GET: utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: utilisateurs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_user,nom_utilisateur,email,nom,prenom,date_naissance,motdepasse,u_role")] utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }

        // GET: utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            utilisateur utilisateur = db.utilisateur.Find(id);
            db.utilisateur.Remove(utilisateur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            
            string username = Request.Form["nom_utilisateur"];
            string password = Request.Form["mot_de_passe"];
            utilisateur user = null;
            try
            {
              user = db.utilisateur.Where(c => c.nom_utilisateur == username && c.motdepasse == password).First();
                Session["user"] = user;
                Session["role"] = user.u_role;
            }catch(System.InvalidOperationException e)
            {
                user = null;
            }
                
                if (user == null)
            {
                ViewBag.connect = "echec de la connexion";
                return View();
            }
            return RedirectToAction("index", "u_application");
        }
        public ActionResult deconnexion()
        {
            Session["user"] = null;
            return RedirectToAction("index", "Home");
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
