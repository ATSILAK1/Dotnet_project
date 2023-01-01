using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class u_applicationController : Controller
    {
        private profinsemEntities2 db = new profinsemEntities2();

        // GET: u_application
        public ActionResult Index()
        {
            var u_application = db.u_application.Include(u => u.categorie1).Include(u => u.utilisateur);
            return View(u_application.ToList());
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
        protected void Upload(object sender)
        {
            //Access the File using the Name of HTML INPUT File.
            HttpPostedFileBase postedFile = Request.Files["FileUpload"];

            //Check if File is available.
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("~/Uploads/") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_app,nom_app,nombre_telechargement,date_ajout,categorie,id_user,app_img,u_description")] u_application u_application)
        {
            
            HttpPostedFileBase postedFile = Request.Files["FileUpload"];
            string filePath=null; 


            if (postedFile != null && postedFile.ContentLength > 0)
            {
                
                filePath = Server.MapPath("~/images/") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
              
            }
            if (ModelState.IsValid)
                {

                u_application.app_img = filePath;
                u_application.date_ajout = DateTime.Now;
                db.u_application.Add(u_application);
                db.SaveChanges();
            return RedirectToAction("Index");
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
            return View(u_application);
        }

        // POST: u_application/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_app,nom_app,nombre_telechargement,date_ajout,categorie,id_user,app_img,u_description")] u_application u_application)
        {
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
