﻿using Domain.Entity;
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
    public class GroupeController : Controller
    {
        // GET: Groupe
        IGroupeService service;
        public GroupeController()
        {

            service = new GroupeService();
        }
        public ActionResult Index(String search, FormCollection form)
        {
            var groupes = service.GetAll();
            List<Groupe> fVM = new List<Groupe>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in groupes)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.nom.ToLower().Contains(search.ToLower())).ToList<Groupe>();


            }
            return View(fVM);   //fVM.Take(10)
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Groupe groupe = service.getById(id);
            return View(groupe);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var groupe = new Groupe();
            return View(groupe);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Groupe c, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            Groupe groupe = new Groupe
            {
                Id = c.Id,
                nom = c.nom,
                responsable=c.responsable
                

            };
            service.Add(groupe);
            service.SaveChange();


            return RedirectToAction("Index");

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Groupe groupe = service.getById(id);

            return View(groupe);
        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditGroupe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var groupe = service.getById(id);

            if (TryUpdateModel(groupe))
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
            return View(groupe);

        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {

            Groupe groupe = service.getById(id);

            service.Delete(groupe);
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

