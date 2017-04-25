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
        public List<SelectListItem> ListDesideur;
        private AppContext db = new AppContext();

        // GET: Authentification
        public ActionResult Index()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
                return View(db.authentifications.ToList());
            else return HttpNotFound();
        }

        // GET: Authentification/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
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
            else
                return HttpNotFound();
        }

        // GET: Authentification/Create
        public ActionResult Create()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {


                var dc = from doc in db.decideursCommune select new { doc.id, doc.nom, doc.prenom, doc.cin };

                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var item in dc)
                {
                    items.Add(new SelectListItem { Text = item.nom + " " + item.prenom, Value = item.cin.ToString() });
                }
                var dg = from doc in db.decideursGouvernorat select new { doc.id, doc.nom, doc.prenom, doc.cin };

                foreach (var item in dg)
                {
                    items.Add(new SelectListItem { Text = item.nom + " " + item.prenom, Value = item.cin.ToString() });
                }
                var com = from doc in db.comptes select new { doc.id, doc.nom, doc.prenom, doc.cin };

                foreach (var item in com)
                {
                    items.Add(new SelectListItem { Text = item.nom + " " + item.prenom, Value = item.cin.ToString() });
                }
                ListDesideur = items;
                ViewBag.listGouv = items;
                return View();
            }
            else
                return HttpNotFound();
        }

        // POST: Authentification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,pseudo,mdp,variable")] Authentification authentification)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (ModelState.IsValid)
                {

                    if (db.getDc(Int32.Parse(authentification.variable)) != null)
                        authentification.decideurCommune = db.getDc(Int32.Parse(authentification.variable));
                    else if (db.getDg(Int32.Parse(authentification.variable)) != null)
                        authentification.decideurGouvernorat = db.getDg(Int32.Parse(authentification.variable));
                    else
                        authentification.adminOuMinitre = db.getAM(Int32.Parse(authentification.variable));

                    db.authentifications.Add(authentification);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(authentification);
            }
            else
                return HttpNotFound();
        }


        // GET: Authentification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
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
            else
                return HttpNotFound();
        }

        // POST: Authentification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)

        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                Authentification authentification = db.authentifications.Find(id);
                db.authentifications.Remove(authentification);
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
