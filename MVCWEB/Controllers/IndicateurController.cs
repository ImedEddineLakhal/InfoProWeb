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
    public class IndicateurController : Controller
    {
        // GET: Indicateur
        IIndicateurService service;
        public IndicateurController()
        {

            service = new IndicateurService();
        }
        public ActionResult Index(String search, FormCollection form)
        {
            var employees = service.GetAll();
            List<Indicateur> fVM = new List<Indicateur>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in employees)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.titre.libelle.ToLower().Contains(search.ToLower())).ToList<Indicateur>();


            }
            return View(fVM);   //fVM.Take(10)
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Indicateur indicateur = service.getById(id);
            return View(indicateur);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var indicateur = new Indicateur();
            return View(indicateur);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Indicateur c, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            Indicateur indicateur = new Indicateur
            {
                Id = c.Id,
                accord = c.accord,
                agent = c.agent,
                appelEmis = c.appelEmis,
                CA = c.CA,
                CNA = c.CNA,
                date = c.date,
                mois = c.mois,
                responsable = c.responsable,
                semaine = c.semaine,
                tAbs = c.tAbs,
                tAbsHebdo = c.tAbsHebdo,
                tAbsMensuel = c.tAbsMensuel,
                tempsACW = c.tempsACW,
                tempsAcwHebdo = c.tempsAcwHebdo,
                tempsAcwmensuel = c.tempsAcwmensuel,
                tempsAtt = c.tempsAtt,
                tempsAtthebdo = c.tempsAtthebdo,
                tempsAttmensuel = c.tempsAttmensuel,
                tempsComm = c.tempsComm,
                tempsLog = c.tempsLog,
                tempsPauseBrief = c.tempsPauseBrief,
                tempsPauseBriefHebdo = c.tempsPauseBriefHebdo,
                tempsPauseBriefMensuel = c.tempsPauseBriefMensuel,
                tempsPausePerso=c.tempsPausePerso,
                tempsPausePersoHebdo=c.tempsPausePersoHebdo,
                tempsPausePersoMensuel=c.tempsPausePersoMensuel,
                tempsPreview=c.tempsPreview,
                tempsPrievewHebdo=c.tempsPrievewHebdo,
                tempsPrievewMensuel=c.tempsPrievewMensuel,
                totAboutis=c.totAboutis,
                tvente=c.tvente,
                tventeHebdo=c.tventeHebdo,
                tventeMensuel=c.tventeMensuel
                

            };
            service.Add(indicateur);
            service.SaveChange();


            return RedirectToAction("Index");

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Indicateur indicateur = service.getById(id);

            return View(indicateur);
        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditIndicateur(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var indicateur = service.getById(id);

            if (TryUpdateModel(indicateur))
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
            return View(indicateur);

        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {

            Indicateur indicateur = service.getById(id);

            service.Delete(indicateur);
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