using ClientLourd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientLourd.Controllers.Login
{
    public class LoginController : Controller
    {
        private AppContext db = new AppContext();
        private Authentification login;
        // GET: Login


        // GET: Authentification/Login
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }
        public ActionResult CreateCommune()
        {

            return RedirectToAction("Create", "Communes");
        }
        public ActionResult IndexCommune()
        {

            return RedirectToAction("Index", "Communes");
        }
        public ActionResult CreateGouvernorat()
        {
            return RedirectToAction("Create", "Gouvernorats");
        }
        public ActionResult IndexGouvernorat()
        {
            return RedirectToAction("Index", "Gouvernorats");
        }
        public ActionResult CreateAuthentification()
        {
            return RedirectToAction("Create", "Authentification");
        }
        public ActionResult IndexAuthentification()
        {
            return RedirectToAction("Index", "Authentification");
        }
        public ActionResult CreateDecideurCommune()
        {
            return RedirectToAction("Create", "DecideurCommunes");
        }
        public ActionResult IndexDecideurCommune()
        {
            return RedirectToAction("Index", "DecideurCommunes");
        }
        public ActionResult IndexDecideurGouvernorat()
        {
            return RedirectToAction("Index", "DecideurGouvernorats");
        }
        public ActionResult CreateDecideurGouvernorat()
        {
            return RedirectToAction("Create", "DecideurGouvernorats");
        }
        public ActionResult CreateAdminOuMinstre()
        {
            return RedirectToAction("Create", "comptes");
        }
        public ActionResult IndexAdminOuMinstre()
        {
            return RedirectToAction("Index", "comptes");
        }
        public ActionResult MinistreBNR(Authentification login)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == true)
            {
                return View("~/Views/Ministre/BNR.cshtml", login);
            }
            else
                return HttpNotFound();
        }

        public ActionResult MinistreBAR(Authentification login)
        {
            if (Session["pseudo"] != null && AppContext.log.adminOuMinitre != null && AppContext.log.adminOuMinitre.ministre == true)
            {
                return View("~/Views/Ministre/BAR.cshtml", login);
            }
            else
                return HttpNotFound();
        }
        public ActionResult ResponsableCommuneBNR()
        {
            if (Session["pseudo"] != null )
            {
                return View("~/Views/ResponsableCommune/BNR.cshtml");
            }
            else
                return HttpNotFound();
        }
        // POST: Authentification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "id,pseudo,mdp,variable")] Authentification authentification)
        {
            if (ModelState.IsValid)
            {
                if (db.login(authentification))
                {
                    Session["pseudo"] = authentification.pseudo;
                    Session["ID"] = authentification.id;
                    login = AppContext.log;
                    //~/Views/Ministre/Index.cshtml
                    if (AppContext.log.adminOuMinitre != null)
                    {
                        if (AppContext.log.adminOuMinitre.ministre)
                            return RedirectToAction("MinistreBNR", login);
                        else
                            return RedirectToAction("CreateCommune", login);
                    }

                    if (AppContext.log.decideurCommune != null)
                        return View("~/Views/Ministre/BNR.cshtml");
                    if (AppContext.log.decideurGouvernorat != null)
                        return View("~/Vieloginws/Ministre/BNR.cshtml");
                    return RedirectToAction("Login");


                    //   return RedirectToAction("Index");

                }



                return RedirectToAction("Login");
            }

            return View("~/Views/Login/Index.cshtml", db.authentifications.ToList());
        }


        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
