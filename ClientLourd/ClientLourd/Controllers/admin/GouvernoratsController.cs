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
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre==false)
            {
                return View("~/Views/admin/Gouvernorats/Index.cshtml", db.gouvernorats.ToList());
            }
            else
                return HttpNotFound();
        }
        // GET: Gouvernorats/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
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
                return View("~/Views/admin/Gouvernorats/Details.cshtml", gouvernorat);
            }
            else
                return HttpNotFound();
        }

        // GET: Gouvernorats/Create
        public ActionResult Create()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                return View("~/Views/admin/Gouvernorats/Create.cshtml");
            }
            else
                return HttpNotFound();
        }

        // POST: Gouvernorats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nomArab,nomfr")] Gouvernorat gouvernorat)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                /*  if (!db.gouvernoratExiste(gouvernorat.nomfr))
                  {
                      ViewBag.msgErreur = "Nom gouvernorat existe déja";
                  }
                  else*/
                if (ModelState.IsValid)
                {
                    db.gouvernorats.Add(gouvernorat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(gouvernorat);
            }
            else
                return HttpNotFound();

        }

        // GET: Gouvernorats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
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
            else
                return HttpNotFound();
        }

        // POST: Gouvernorats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nomArab,nomfr")] Gouvernorat gouvernorat)

        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                /* if (!db.gouvernoratExiste(gouvernorat.nomfr))
                 {
                     ViewBag.msgErreur = "Nom gouvernorat existe déja";
                 }
                 else */
                if (ModelState.IsValid)
                {
                    db.Entry(gouvernorat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(gouvernorat);
            }
            else
                return HttpNotFound();
        }

        // GET: Gouvernorats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
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
            else
                return HttpNotFound();
        }

        // POST: Gouvernorats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["pseudo"] != null &&AppContext.log.adminOuMinitre!= null && AppContext.log.adminOuMinitre.ministre == false)
            {
                Gouvernorat gouvernorat = db.gouvernorats.Find(id);
                if (gouvernorat.decideurs != null)
                    db.effaceGouv(gouvernorat);
                db.gouvernorats.Remove(gouvernorat);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}
