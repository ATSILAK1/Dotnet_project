using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using dev.Models;

namespace dev.Controllers
{
    public class PilotesController : Controller
    {
        private BD_aeroportEntities db = new BD_aeroportEntities();

        // GET: Pilotes
        public ActionResult Index()
        {
            //var pquery = db.Pilote.Where(elt => elt.nom_pil.Contains("i")).ToList();
            // var pquery = db.Pilote.Where(elt => elt.ville_domicile == "Rabat" && elt.salaire > 18000).ToList();
            //var pquery = db.Pilote.OrderByDescending(c => c.salaire);
            // var pquery = db.Vol.Where(elt=> elt.date_depart.CompareTo(new DateTime(2017,9,1)) < 0 && elt.date_depart.CompareTo(new DateTime(2017, 9, 1))>0);
            //var pquery = db.Pilote.Where(elt => (elt.salaire < 20000) && (elt.ville_domicile == "casablanca")).ToList();
            var pquery = from pi in db.Pilote 
                         join vo in db.Vol 
                         on pi.num_pil equals vo.num_pil
                         where (vo.ville_depart == "rabat" && (vo.ville_arrive == "Paris" || vo.ville_arrive == "Madrid"))
                         select pi
                         ;
            
            ViewData["max"] = db.Pilote.Max(elt => elt.salaire).ToString();
            ViewData["min"] = db.Pilote.Min(elt => elt.salaire).ToString();
            ViewData["moyenne"] = db.Pilote.Average(elt => elt.salaire).ToString();
            ViewData["total"] = db.Pilote.Sum(elt => elt.salaire).ToString();
           


            return View(db.Pilote.ToList());
        }
        public ActionResult Trier()
        {
            var query = from pi in db.Pilote orderby pi.ville_domicile, pi.salaire select pi;
            return View(query.ToList());
        }
        

        // GET: Pilotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilote pilote = db.Pilote.Find(id);
            if (pilote == null)
            {
                return HttpNotFound();
            }
            return View(pilote);
        }

        // GET: Pilotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pilotes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "num_pil,nom_pil,prenom_pil,ville_domicile,num_tel,email,salaire")] Pilote pilote)
        {
            if (ModelState.IsValid)
            {
                db.Pilote.Add(pilote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pilote);
        }

        // GET: Pilotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilote pilote = db.Pilote.Find(id);
            if (pilote == null)
            {
                return HttpNotFound();
            }
            return View(pilote);
        }

        // POST: Pilotes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "num_pil,nom_pil,prenom_pil,ville_domicile,num_tel,email,salaire")] Pilote pilote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pilote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pilote);
        }

        // GET: Pilotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilote pilote = db.Pilote.Find(id);
            if (pilote == null)
            {
                return HttpNotFound();
            }
            return View(pilote);
        }

        // POST: Pilotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pilote pilote = db.Pilote.Find(id);
            db.Pilote.Remove(pilote);
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
