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
    public class PlaningController : Controller
    {
        // GET: Planing
        IPlaningService service;
        public PlaningController()
        {

            service = new PlaningService();
        }
        public ActionResult Index(String search, FormCollection form)
        {
            var planings = service.GetAll();
            List<Planing> fVM = new List<Planing>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in planings)
            {
                fVM.Add(item);
            }
            //if (!String.IsNullOrEmpty(search))
            //{

            //    fVM = fVM.Where(p => p.login.ToLower().Contains(search.ToLower())).ToList<Employee>();


            //}
            return View(fVM);   //fVM.Take(10)
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Planing planing = service.getById(id);
            return View(planing);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var planing = new Planing();
            return View(planing);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Planing c, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            Planing planing = new Planing
            {
                Id = c.Id,
                date = c.date,
                dateDepart = c.dateDepart,
                heureArrivee = c.heureArrivee,
                duree=c.duree

            };
            service.Add(planing);
            service.SaveChange();


            return RedirectToAction("Index");

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Planing planing = service.getById(id);

            return View(planing);
        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var planing = service.getById(id);

            if (TryUpdateModel(planing))
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
            return View(planing);

        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {

            Planing planing = service.getById(id);

            service.Delete(planing);
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
    }
}
