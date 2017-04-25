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
    public class CommunesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Communes
        public ActionResult Index()
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                return View(db.communes.ToList());
            }
            else
                return HttpNotFound();
        }

        // GET: Communes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Commune commune = db.communes.Find(id);
                if (commune == null)
                {
                    return HttpNotFound();
                }
                return View(commune);
            }
            else
                return HttpNotFound();
        }

        // GET: Communes/Create
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

        // POST: Communes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nomArab,nomfr,selectedGouv")] Commune commune)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (!db.communeExiste(commune.nomfr))
                {
                    ViewBag.msgErreur = "Nom commune existe déja";
                }
                else if (ModelState.IsValid)
                {
                    Gouvernorat g = db.gouvernorats.Find(Int32.Parse(commune.selectedGouv));
                    commune.gouvernorat = g;
                    db.communes.Add(commune);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(commune);
            }
            else
                return HttpNotFound();
        }

        // GET: Communes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Commune commune = db.communes.Find(id);
                if (commune == null)
                {
                    return HttpNotFound();
                }
                Gouvernorat g = db.gouvernorats.Find(commune.gouvernorat.id);
                var gouvernoratQuery = from doc in db.gouvernorats select new { doc.id, doc.nomfr };
                List<SelectListItem> k = new List<SelectListItem>();
                foreach (var c in gouvernoratQuery)
                {
                    if (c.id == g.id)
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
                return View(commune);
            }
            else
                return HttpNotFound();

        }

        // POST: Communes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nomArab,nomfr,selectedGouv")] Commune commune)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (!db.communeExiste(commune.nomfr))
                {
                    ViewBag.msgErreur = "Nom commune existe déja";
                }
                else if (ModelState.IsValid)
                {
                    Gouvernorat g = db.gouvernorats.Find(Int32.Parse(commune.selectedGouv));
                    Commune dg = db.communes.Find(commune.id);
                    db.communes.Attach(dg);
                    db.gouvernorats.Attach(g);
                    dg.nomfr = commune.nomfr;
                    dg.selectedGouv = commune.selectedGouv;
                    dg.gouvernorat = g;


                    db.Entry(dg).State = EntityState.Modified;





                    db.SaveChanges();
                    return RedirectToAction("Index");


                }
                return View(commune);
            }
            else
                return HttpNotFound();
        }

        // GET: Communes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Commune commune = db.communes.Find(id);
                if (commune == null)
                {
                    return HttpNotFound();
                }
                return View(commune);
            }
            else
                return HttpNotFound();
        }

        // POST: Communes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == false)
            {
                Commune commune = db.communes.Find(id);
                db.communes.Remove(commune);
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
