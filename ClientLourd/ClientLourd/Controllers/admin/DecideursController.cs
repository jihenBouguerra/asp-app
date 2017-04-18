using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClientLourd.Models;

namespace ClientLourd.Controllers.admin
{
    public class DecideursController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Decideurs
        public ActionResult Index()
        {
            return View(db.decideurs.ToList());
        }

        // GET: Decideurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decideur decideur = db.decideurs.Find(id);
            if (decideur == null)
            {
                return HttpNotFound();
            }
            return View(decideur);
        }

        // GET: Decideurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Decideurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cin,nom,prenom,Email")] Decideur decideur)
        {
            
            if (ModelState.IsValid)
            {
                if (db.decideurs.Any(o => o.cin == decideur.cin))
                {
                    ViewBag.msgErreur = "cin existe";
                }
                else
                {
                    db.decideurs.Add(decideur);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }


            return View(decideur);
        }

        // GET: Decideurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decideur decideur = db.decideurs.Find(id);
            if (decideur == null)
            {
                return HttpNotFound();
            }
            return View(decideur);
        }

        // POST: Decideurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cin,nom,prenom,Email")] Decideur decideur)
        {
            if (ModelState.IsValid)
            {
                

                if (db.decideurs.Any(o => o.cin == decideur.cin))
                {
                    ViewBag.msgErreur = "cin existe";
                }
                else
                {
                    db.Entry(decideur).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            return View(decideur);
        }

        // GET: Decideurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decideur decideur = db.decideurs.Find(id);
            if (decideur == null)
            {
                return HttpNotFound();
            }
            return View(decideur);
        }

        // POST: Decideurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Decideur decideur = db.decideurs.Find(id);
            db.decideurs.Remove(decideur);
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
