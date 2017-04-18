using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PFEProjet.Controllers
{
    public class MinistreController : Controller
    {
        // GET: Ministre
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ministre/bnr
        public ActionResult BNR()
        {
            return View();
        }
        // GET: Ministre/bnr
        public ActionResult BAR()
        {
            return View();
        }

        // GET: Ministre/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ministre/Create
        public ActionResult Create()
        {
            return View();
        }
       

        // POST: Ministre/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ministre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ministre/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ministre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ministre/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
