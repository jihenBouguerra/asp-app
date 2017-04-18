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
            return View(db.communes.ToList());
        }

        // GET: Communes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Communes/Create
        public ActionResult Create()
        {
            var gouvernoratQuery = from doc in db.gouvernorats select new { doc.id, doc.nomfr};
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

        // POST: Communes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nomArab,nomfr,selectedGouv")] Commune commune)
        {
            if (ModelState.IsValid)
            {
                Gouvernorat g = db.gouvernorats.Find(Int32.Parse(commune.selectedGouv));
                commune.gouvernorat = g;
                db.communes.Add(commune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commune);
        }

        // GET: Communes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commune commune = db.communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }else
            {
                var gouvernoratQuery = from doc in db.gouvernorats select new { doc.id, doc.nomfr };
                 List<SelectListItem> k = new List<SelectListItem>();
                foreach (var c in gouvernoratQuery)
                {   if (c.id ==id) {
                        k.Add(new SelectListItem {
                            Text = c.nomfr,
                            Value = c.id.ToString(),
                            Selected = true
                        }) ;
                    }else{
                        k.Add(new SelectListItem
                        {
                            Text = c.nomfr,
                            Value = c.id.ToString(),
                        });
                        
                    }
                     

                }

                
                ViewBag.listGouv =k;
                return View(commune);
            }
           
        }

        // POST: Communes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nomArab,nomfr,selectedGouv")] Commune commune)
        {
            if (ModelState.IsValid)
            {

                Gouvernorat g = db.gouvernorats.Find(Int32.Parse(commune.selectedGouv));
                commune.gouvernorat = g;
                var entity = db.communes.Find(commune.id);
                     
                db.Entry(entity).CurrentValues.SetValues(commune);
             

               // db.Entry(commune).State = EntityState.Modified;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commune);
        }

        // GET: Communes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Communes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commune commune = db.communes.Find(id);
            db.communes.Remove(commune);
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
