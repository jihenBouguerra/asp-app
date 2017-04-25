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
    public class DecideurCommunesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: DecideurCommunes
        public ActionResult Index()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                return View(db.decideursCommune.ToList());
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurCommunes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DecideurCommune decideurCommune = db.decideursCommune.Find(id);
                if (decideurCommune == null)
                {
                    return HttpNotFound();
                }
                return View(decideurCommune);
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurCommunes/Create
        public ActionResult Create()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                var gouvernoratQuery = from doc in db.communes select new { doc.id, doc.nomfr };
                IEnumerable<SelectListItem> i =
                    from c in gouvernoratQuery
                    select new SelectListItem
                    {

                        Text = c.nomfr,
                        Value = c.id.ToString()
                    };
                List<SelectListItem> items = new List<SelectListItem>();

                foreach (var item in gouvernoratQuery)
                {
                    items.Add(new SelectListItem { Text = item.nomfr, Value = item.id.ToString() });
                }
                ViewBag.listGouv = items;
                return View();
            }
            else
                return HttpNotFound();
        }

        // POST: DecideurCommunes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cin,nom,prenom,variable,Email")] DecideurCommune decideurCommune)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (ModelState.IsValid)
                {
                    Commune g = db.communes.Find(Int32.Parse(decideurCommune.variable));
                    decideurCommune.commune = g;
                    db.decideursCommune.Add(decideurCommune);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(decideurCommune);
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurCommunes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DecideurCommune decideurCommune = db.decideursCommune.Find(id);
                if (decideurCommune == null)
                {
                    return HttpNotFound();
                }
                var idgou = decideurCommune.commune.id;
                var gouvernoratQuery = from doc in db.communes select new { doc.id, doc.nomfr };
                List<SelectListItem> k = new List<SelectListItem>();
                foreach (var c in gouvernoratQuery)
                {
                    if (c.id == idgou)
                    {
                        k.Add(new SelectListItem
                        {
                            Text = c.nomfr,
                            Value = c.id.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        k.Add(new SelectListItem
                        {
                            Text = c.nomfr,
                            Value = c.id.ToString(),
                        });

                    }


                }
                ViewBag.listGouv = k;
                return View(decideurCommune);
            }
            else
                return HttpNotFound();

        }

        // POST: DecideurCommunes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cin,nom,prenom,variable,Email")] DecideurCommune decideurCommune)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (ModelState.IsValid)
                {


                    Commune g = db.communes.Find(Int32.Parse(decideurCommune.variable));
                    DecideurCommune dg = db.decideursCommune.Find(decideurCommune.id);

                    db.decideursCommune.Attach(dg);
                    db.communes.Attach(g);

                    dg.Email = decideurCommune.Email;
                    dg.cin = decideurCommune.cin;
                    dg.nom = decideurCommune.nom;
                    dg.prenom = decideurCommune.prenom;
                    dg.variable = decideurCommune.variable;



                    dg.commune = g;
                    db.Entry(dg).State = EntityState.Modified;

                    /*  Gouvernorat g = db.gouvernorats.Find(Int32.Parse(decideurGouvernorat.variable));
                      DecideurGouvernorat dg = db.decideursGouvernorat.Find(decideurGouvernorat.id);
                      dg.Email = decideurGouvernorat.Email;
                      dg.cin = decideurGouvernorat.cin;
                      dg.nom = decideurGouvernorat.nom;
                      dg.prenom = decideurGouvernorat.prenom;
                      dg.variable = decideurGouvernorat.variable;
                      db.decideursGouvernorat.Attach(dg);

                      db.gouvernorats.Attach(g);
                      dg.gouvernorat = g;

                      db.Entry(dg).State = EntityState.Modified;
                      */







                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(decideurCommune);
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurCommunes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DecideurCommune decideurCommune = db.decideursCommune.Find(id);
                if (decideurCommune == null)
                {
                    return HttpNotFound();
                }
                return View(decideurCommune);
            }
            else
                return HttpNotFound();
        }

        // POST: DecideurCommunes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                DecideurCommune decideurCommune = db.decideursCommune.Find(id);
                db.decideursCommune.Remove(decideurCommune);
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
