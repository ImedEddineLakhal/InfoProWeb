using Domain.Entity;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWEB.Controllers
{
    public class AlerteController : Controller
    {
        // GET: Alerte
        IAlerteService service;
        public AlerteController()
        {

            service = new AlerteService();
        }
        public ActionResult Index(String search, FormCollection form)
        {
            string value = (string)Session["loginIndex"];
            var alertes = service.GetAll();
            List<Alerte> fVM = new List<Alerte>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in alertes)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.titreAlerte.ToLower().Contains(search.ToLower())).ToList<Alerte>();


            }
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                return View(fVM);   //fVM.Take(10)
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Alerte alerte = service.getById(id);
            return View(alerte);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var alerte = new Alerte();
            return View(alerte);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alerte c, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            Alerte alerte = new Alerte
            {
                Id = c.Id,
                dateCreation = c.dateCreation,
                dateReponse = c.dateReponse,
                description = c.description,
                etatAlerte=c.etatAlerte,
                reponseAlerte=c.reponseAlerte,
                titreAlerte=c.titreAlerte
                

            };
            service.Add(alerte);
            service.SaveChange();


            return RedirectToAction("Index");

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Alerte alerte = service.getById(id);

            return View(alerte);
        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditAlerte(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alerte = service.getById(id);

            if (TryUpdateModel(alerte))
            {
                try
                {
                    service.SaveChange();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Erreur!!!!!");
                }
            }
            return View(alerte);

        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {

            Alerte alerte = service.getById(id);

            service.Delete(alerte);
            service.SaveChange();
            return RedirectToAction("Index");
        }

        // POST: Medcin/Delete/5
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
        [HttpGet]
        public ActionResult FindAlerte(int? Id)
        {
            Alerte item = service.getById(Id);


            //var a = new EmployeeViewModel();
            //a.Id = item.Id;
            //a.userName = item.userName;
            //a.pseudoName = item.pseudoName;
            //a.IdAD = (int)item.userId;
            //a.IdHermes = item.IdHermes;
            //a.Activite = item.Activite;
            //a.role = item.role;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AlerteDeSuppression", item);
            }

            else
            {
                return View(item);
            }
        }
    }
}
