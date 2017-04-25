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
    public class DecideurGouvernoratsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: DecideurGouvernorats
        public ActionResult Index()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                return View(db.decideursGouvernorat.ToList());
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurGouvernorats/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DecideurGouvernorat decideurGouvernorat = db.decideursGouvernorat.Find(id);
                if (decideurGouvernorat == null)
                {
                    return HttpNotFound();
                }
                return View(decideurGouvernorat);
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurGouvernorats/Create
        public ActionResult Create()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                var gouvernoratQuery = from doc in db.gouvernorats select new { doc.id, doc.nomfr };
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

        // POST: DecideurGouvernorats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cin,nom,prenom,variable,Email")] DecideurGouvernorat decideurGouvernorat)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (ModelState.IsValid)
                {
                    Gouvernorat g = db.gouvernorats.Find(Int32.Parse(decideurGouvernorat.variable));
                    decideurGouvernorat.gouvernorat = g;
                    db.decideursGouvernorat.Add(decideurGouvernorat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(decideurGouvernorat);
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurGouvernorats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DecideurGouvernorat decideurGouvernorat = db.decideursGouvernorat.Find(id);

                if (decideurGouvernorat == null)
                {
                    return HttpNotFound();
                }
                var idgou = decideurGouvernorat.gouvernorat.id;
                var gouvernoratQuery = from doc in db.gouvernorats select new { doc.id, doc.nomfr };
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
                return View(decideurGouvernorat);
            }
            else
                return HttpNotFound();
        }

        // POST: DecideurGouvernorats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cin,nom,prenom,variable,Email")] DecideurGouvernorat decideurGouvernorat)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (ModelState.IsValid)
                {


                    Gouvernorat g = db.gouvernorats.Find(Int32.Parse(decideurGouvernorat.variable));
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





                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(decideurGouvernorat);
            }
            else
                return HttpNotFound();
        }

        // GET: DecideurGouvernorats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DecideurGouvernorat decideurGouvernorat = db.decideursGouvernorat.Find(id);
                if (decideurGouvernorat == null)
                {
                    return HttpNotFound();
                }


                return View(decideurGouvernorat);
            }
            else
                return HttpNotFound();
        }

        // POST: DecideurGouvernorats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                DecideurGouvernorat decideurGouvernorat = db.decideursGouvernorat.Find(id);
                foreach (var c in decideurGouvernorat.authentifications.ToList())
                {
                    Authentification a = db.authentifications.Find(c.id);
                    db.authentifications.Remove(a);
                }
                db.decideursGouvernorat.Remove(decideurGouvernorat);
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
