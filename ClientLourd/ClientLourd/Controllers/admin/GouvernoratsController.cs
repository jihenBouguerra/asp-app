using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClientLourd.Models;

namespace ClientLourd.Controllers
{
    public class GouvernoratsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Gouvernorats
        public ActionResult Index()
        {
            return View("~/Views/admin/Gouvernorats/Index.cshtml", db.gouvernorats.ToList());
        }

        // GET: Gouvernorats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gouvernorat gouvernorat = db.gouvernorats.Find(id);
            if (gouvernorat == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/admin/Gouvernorats/Details.cshtml",gouvernorat);
        }

        // GET: Gouvernorats/Create
        public ActionResult Create()
        {
            return View("~/Views/admin/Gouvernorats/Create.cshtml");
        }

        // POST: Gouvernorats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nomArab,nomfr")] Gouvernorat gouvernorat)
        {
            if (ModelState.IsValid)
            {
                db.gouvernorats.Add(gouvernorat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gouvernorat);
        }

        // GET: Gouvernorats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gouvernorat gouvernorat = db.gouvernorats.Find(id);
            if (gouvernorat == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/admin/Gouvernorats/Edit.cshtml", gouvernorat);
        }

        // POST: Gouvernorats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nomArab,nomfr")] Gouvernorat gouvernorat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gouvernorat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gouvernorat);
        }

        // GET: Gouvernorats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gouvernorat gouvernorat = db.gouvernorats.Find(id);
            if (gouvernorat == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/admin/Gouvernorats/Delete.cshtml", gouvernorat);
        }

        // POST: Gouvernorats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gouvernorat gouvernorat = db.gouvernorats.Find(id);
            db.gouvernorats.Remove(gouvernorat);
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
