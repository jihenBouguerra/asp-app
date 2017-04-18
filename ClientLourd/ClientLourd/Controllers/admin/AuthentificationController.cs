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
    public class AuthentificationController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Authentification
        public ActionResult Index()
        {
            return View(db.authentifications.ToList());
        }

        // GET: Authentification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentification authentification = db.authentifications.Find(id);
            if (authentification == null)
            {
                return HttpNotFound();
            }
            return View(authentification);
        }

        // GET: Authentification/Create
        public ActionResult Create()
        {
            var gouvernoratQuery = from doc in db.decideurs select new { doc.id, doc.cin, doc.nom, doc.prenom };
            IEnumerable<SelectListItem> i =
                from c in gouvernoratQuery
                select new SelectListItem
                {

                    Text = c.prenom,
                    Value = c.id.ToString()
                };
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in gouvernoratQuery)
            {
                items.Add(new SelectListItem { Text = item.nom, Value = item.id.ToString() });
            }
            ViewBag.listDecideur = items;

            return View();
        }

        // POST: Authentification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,pseudo,mdp,idDecideur")] Authentification authentification)
        {
            if (ModelState.IsValid)
            {
                Decideur d = db.decideurs.Find(Int32.Parse(authentification.idDecideur));
                authentification.decideur = d;
                db.authentifications.Add(authentification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authentification);
        }

        // GET: Authentification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentification authentification = db.authentifications.Find(id);
            if (authentification == null)
            {
                return HttpNotFound();
            }
            return View(authentification);
        }

        // POST: Authentification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,pseudo,mdp,idDecideur")] Authentification authentification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authentification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authentification);
        }

        // GET: Authentification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentification authentification = db.authentifications.Find(id);
            if (authentification == null)
            {
                return HttpNotFound();
            }
            return View(authentification);
        }

        // POST: Authentification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Authentification authentification = db.authentifications.Find(id);
            db.authentifications.Remove(authentification);
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
