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
    public class comptesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: comptes
        public ActionResult Index()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                return View(db.comptes.ToList());
            }
            else
                return HttpNotFound();
        }

        // GET: comptes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null && AppContext.log.adminOuMinitre != null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                compte compte = db.comptes.Find(id);
                if (compte == null)
                {
                    return HttpNotFound();
                }
                return View(compte);
            }
            else
                return HttpNotFound();
        }

        // GET: comptes/Create
        public ActionResult Create()
        {
            return View();
            /*
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                return View();
            }
            else
                return HttpNotFound();*/
        }

        // POST: comptes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cin,nom,prenom,variable,Email,ministre")] compte compte)
        {
          /*  if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {*/
                if (ModelState.IsValid)
                {
                    db.comptes.Add(compte);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(compte);
           /* }
            else
                return HttpNotFound();*/
        }

        // GET: comptes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                compte compte = db.comptes.Find(id);
                if (compte == null)
                {
                    return HttpNotFound();
                }
                return View(compte);
            }
            else
                return HttpNotFound();
        }

        // POST: comptes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cin,nom,prenom,variable,Email,ministre")] compte compte)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {

                if (ModelState.IsValid)
                {
                    db.Entry(compte).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(compte);
            }
            else
                return HttpNotFound();
        }

        // GET: comptes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                compte compte = db.comptes.Find(id);
                if (compte == null)
                {
                    return HttpNotFound();
                }
                return View(compte);
            }
            else
                return HttpNotFound();
        }

        // POST: comptes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                compte compte = db.comptes.Find(id);
                db.comptes.Remove(compte);
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
