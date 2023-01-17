using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using WebApplication1.Models;
using static System.Net.WebRequestMethods;

namespace WebApplication1.Controllers
{
    public class u_applicationController : Controller
    {
        private profinsemEntities db = new profinsemEntities();

        // GET: u_application
        public ActionResult Index()
        {
            var u_application = db.u_application.Include(u => u.categorie1).Include(u => u.utilisateur);
            return View(u_application.ToList());
        }
        [HttpPost]
        public ActionResult Index(string recherche)
        {
            return View(db.u_application.Where(c => c.nom_app.Contains(recherche)).ToList());
        }
        // GET: u_application/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            u_application u_application = db.u_application.Find(id);
            if (u_application == null)
            {
                return HttpNotFound();
            }
            return View(u_application);
        }

        // GET: u_application/Create
        public ActionResult Create()
        {
            ViewBag.categorie = new SelectList(db.categorie, "id_cat", "nom_categorie");
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur");
            return View();
        }

        // POST: u_application/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_app,nom_app,nombre_telechargement,date_ajout,categorie,id_user,app_img,u_description,img")] u_application u_application )
        {
            var user = Session["user"] as utilisateur;
            string path = Server.MapPath("~/images");
            string filename = Path.GetFileName(u_application.img.FileName);
            string fullpath = Path.Combine(path, filename);
            u_application.img.SaveAs(fullpath);
            u_application.app_img = "/images/" + filename;
            u_application.date_ajout = DateTime.Now.Date;
            u_application.nombre_telechargement = 0;
            u_application.id_user = user.id_user;
            if (user.u_role != "admin")
                user.u_role = "prop";
            if (ModelState.IsValid)
            {
                

                db.u_application.Add(u_application);
                db.SaveChanges();
            return RedirectToAction("Index","u_application");
            }

            ViewBag.categorie = new SelectList(db.categorie, "id_cat", "nom_categorie", u_application.categorie);
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur", u_application.id_user);
            return View(u_application);
        }

        // GET: u_application/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            u_application u_application = db.u_application.Find(id);
            if (u_application == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorie = new SelectList(db.categorie, "id_cat", "nom_categorie", u_application.categorie);
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur", u_application.id_user);
            TempData["id_app"] = id; 
            return View(u_application);
        }

        // POST: u_application/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_app,nom_app,nombre_telechargement,date_ajout,categorie,id_user,img,app_img,u_description")] u_application u_application)
        {
            //var app = db.u_application.Find(TempData["id_app"]);
            //System.IO.File.Delete(Server.MapPath("~/")+app.app_img);
            string path = Server.MapPath("~/images");
            string filename = Path.GetFileName(u_application.img.FileName);
            string fullpath = Path.Combine(path, filename);
            u_application.img.SaveAs(fullpath);
            u_application.app_img = "/images/" + filename;
            

            if (ModelState.IsValid)
            {
                
                db.Entry(u_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categorie = new SelectList(db.categorie, "id_cat", "nom_categorie", u_application.categorie);
            ViewBag.id_user = new SelectList(db.utilisateur, "id_user", "nom_utilisateur", u_application.id_user);
            return View(u_application);
        }

        // GET: u_application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            u_application u_application = db.u_application.Find(id);
            if (u_application == null)
            {
                return HttpNotFound();
            }
            return View(u_application);
        }

        // POST: u_application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            u_application u_application = db.u_application.Find(id);
            db.u_application.Remove(u_application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult telechargerapp(int id)
        {
            try
            {
                var u_application = db.u_application.Find(id);
                utilisateur user  = null;
                bibliotheque bib  = null ;
                u_application.nombre_telechargement++;
                try
                {
                    user = Session["user"] as utilisateur;
                    bib  = db.bibliotheque.Find(user.bibliotheque_id_bib);
                }catch(System.NullReferenceException)
                {
                    return RedirectToAction("login", "utilisateurs");
                }
                Bibliotheque_app appbib = new Bibliotheque_app();
                db.Bibliotheque_app.Find(user.bibliotheque_id_bib, u_application.id_app);
                string path = Server.MapPath("~/txt/");


                Response.ContentType = "application/txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename="+u_application.nom_app+".txt");
                Response.TransmitFile(path+"TextFile1.txt");
                Response.End();


                try
                {
                    appbib.u_application = u_application;
                    appbib.bibliotheque = bib;
                    appbib.date_telechargement = DateTime.Now.Date;
                    db.Bibliotheque_app.Add(appbib);
                    db.SaveChanges();
                    return RedirectToAction("index", "u_application");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {

                    db.u_application.AddOrUpdate(u_application);
                    return RedirectToAction("index", "u_application");
                }
                
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                
            }
            return View();
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
