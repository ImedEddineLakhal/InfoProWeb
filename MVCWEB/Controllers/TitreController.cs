using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using Services;
using Domain.Entity;

namespace MVCWEB.Controllers
{
    public class TitreController : Controller
    {

        ITitreService service;
        public TitreController()
        {

            service = new TitreService();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Titre titre = service.getById(id);
            return View(titre);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createe(Titre t, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            Titre titre = new Titre
            {
                Id = t.Id,
                activite = t.activite,
                type = t.type,
                libelle = t.libelle,
                codeOperation = t.codeOperation,
                codeProvRelance = t.codeProvRelance,
                dateInjection = t.dateInjection,
                nombreFichesInjectees = t.nombreFichesInjectees

            };
            service.Add(titre);
            service.SaveChange();
            return RedirectToAction("List");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Titre titre = service.getById(id);

            return View(titre);
        }


        [HttpPost, ActionName("Edit")]
        public ActionResult EditTitre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var titre = service.getById(id);

            if (TryUpdateModel(titre))
            {
                try
                {
                    service.SaveChange();
                    return RedirectToAction("List");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "error inputs");
                }
            }
            return View(titre);

        }

        public ActionResult list(String search, FormCollection form)
        {
            var titres = service.GetAll();
            List<Titre> fVM = new List<Titre>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in titres)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(t => t.libelle.ToLower().Contains(search.ToLower())).ToList<Titre>();


            }
            return View(fVM);   //fVM.Take(10)
        }


        public ActionResult Delete(int? id)
        {

            Titre titre = service.getById(id);

            service.Delete(titre);
            service.SaveChange();
            return RedirectToAction("List");
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult FindTitre(int? Id)
        {
            Titre item = service.getById(Id);


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
