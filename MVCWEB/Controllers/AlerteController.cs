using Domain.Entity;
using Microsoft.AspNet.SignalR;
using MVCWEB.Models;
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
        IEmployeeService serviceEmployee;
        IGroupeEmployeeService serviceGroupeEmp;
        public AlerteController()
        {

            service = new AlerteService();
            serviceEmployee = new EmployeeService();
            serviceGroupeEmp = new GroupesEmployeService();
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
                etatAlerte = c.etatAlerte,
                reponseAlerte = c.reponseAlerte,
                titreAlerte = c.titreAlerte


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
        public ActionResult IndexAgentManager(String search, FormCollection form)
        {
            string value = (string)Session["loginIndex"];
            Employee emp = serviceEmployee.getByLoginUser(value);
            List<AlerteViewModel> a = new List<AlerteViewModel>();
            var alertes = service.GetAll();
            List<Alerte> fVM = new List<Alerte>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in alertes)
            {
                AlerteViewModel test = new AlerteViewModel();
                test.titreAlerte = item.titreAlerte;
                test.description = item.description;
                if (item.senderId != null)
                {
                    test.senderId = item.senderId;
                    Employee sender = serviceEmployee.getById(item.senderId);
                    test.senderName = sender.userName;
                    test.Id = item.Id;
                    test.dateCreation = item.dateCreation;
                    test.reponseAlerte = item.reponseAlerte;

                }
                if (item.reciverId != null)
                {
                    test.reciverId = item.reciverId;
                    Employee reciver = serviceEmployee.getById(item.reciverId);
                    test.reciverName = reciver.userName;
                    test.Id = item.Id;
                    test.dateCreation = item.dateCreation;
                    test.reponseAlerte = item.reponseAlerte;

                }
                a.Add(test);
            }


            a = a.Where(p => p.senderId == emp.Id).ToList<AlerteViewModel>();



            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                return View(a);   //fVM.Take(10)
            }
        }
        public ActionResult IndexAgentManagerReciver(String search, FormCollection form)
        {
            string value = (string)Session["loginIndex"];
            Employee emp = serviceEmployee.getByLoginUser(value);
            List<AlerteViewModel> a = new List<AlerteViewModel>();
            var alertes = service.GetAll();
            List<Alerte> fVM = new List<Alerte>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in alertes)
            {
                AlerteViewModel test = new AlerteViewModel();
                test.titreAlerte = item.titreAlerte;
                test.description = item.description;
                if (item.senderId != null)
                {
                    Employee sender = serviceEmployee.getById(item.senderId);
                    test.senderId = item.senderId;
                    test.senderName = sender.userName;
                    test.Id = item.Id;
                    test.dateCreation = item.dateCreation;


                }
                if (item.reciverId != null)
                {
                    Employee reciver = serviceEmployee.getById(item.reciverId);
                    test.reciverName = reciver.userName;
                    test.reciverId = item.reciverId;
                    test.Id = item.Id;
                    test.dateCreation = item.dateCreation;

                }
                a.Add(test);
            }


            a = a.Where(p => p.reciverId == emp.Id).ToList<AlerteViewModel>();
            int b = a.Count;


            if (value == null)
            {
                ViewBag.nb = b;
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                ViewBag.alertes = a;
                return View(a);   //fVM.Take(10)
            }
        }
        public ActionResult IndexAgentRecived(String search, FormCollection form)
        {
            string value = (string)Session["loginIndex"];
            Employee emp = serviceEmployee.getByLoginUser(value);
            List<AlerteViewModel> a = new List<AlerteViewModel>();
            var alertes = service.GetAll();
            List<Alerte> fVM = new List<Alerte>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in alertes)
            {
                AlerteViewModel test = new AlerteViewModel();
                test.titreAlerte = item.titreAlerte;
                test.description = item.description;
                if (item.senderId != null)
                {
                    Employee sender = serviceEmployee.getById(item.senderId);
                    test.senderId = item.senderId;
                    test.senderName = sender.userName;
                    test.status = item.status;
                    test.Id = item.Id;
                    test.dateCreation = item.dateCreation;
                }
                if (item.reciverId != null)
                {
                    Employee reciver = serviceEmployee.getById(item.reciverId);
                    test.reciverName = reciver.userName;
                    test.reciverId = item.reciverId;
                    test.status = item.status;
                    test.Id = item.Id;
                    test.dateCreation = item.dateCreation;

                }
                a.Add(test);
            }


            List<AlerteViewModel> recivedAlertes = a.Where(p => p.reciverId == emp.Id).ToList<AlerteViewModel>();
            a = a.Where(p => p.reciverId == emp.Id && p.status == "NotSeen").ToList<AlerteViewModel>();
            int b = 0;
            if (a != null)
            {
                b = a.Count;

            }
            else
            {
                a = null;
                b = 0;

            }
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                ViewBag.nb = b;
                ViewBag.alertes = a;
                return View(recivedAlertes);   //fVM.Take(10)
            }
        }
        public ActionResult CreateAlerteManager()
        {
            String login = Session["loginIndex"].ToString();
            Employee item = serviceEmployee.getByLoginUser(login);
            var a = new AlerteViewModel();
            var usersAssociees = serviceGroupeEmp.getListEmployeeByGroupe(item.Id);
            var tests = usersAssociees.Select(o => o.userLogin).Distinct().ToList();
            foreach (String test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Value = test, Text = test });
            }
            return View(a);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAlerteManager(AlerteViewModel a, string Id ,string dateCreation ,FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("CreateAlerteManager");
            }
            String login = Session["loginIndex"].ToString();
            Employee item = serviceEmployee.getByLoginUser(login);

            string username = form["typeGenerator"].ToString();
            //string message = form["message"].ToString();
            string titre = form["titre"].ToString();
            string description = form["description"].ToString();
            string dateTest = form["dateCreation"].ToString();
            DateTime dateCreationtest = DateTime.Parse(dateTest);
              
            Employee emp = serviceEmployee.getByLoginUser(username);
            //List<Alerte> alertes = service.getByDate(dateCreationtest, emp.Id);
          
                Alerte alerte = new Alerte
                {

                    description = description,
                    //reponseAlerte = message,
                    titreAlerte = titre,
                    reciverId = emp.Id,
                    senderId = item.Id,
                    dateCreation = dateCreationtest,
                    status = "NotSeen"


                };
                service.Add(alerte);
                service.SaveChange();


                return RedirectToAction("IndexAgentManager");
            
           
        }
        public ActionResult AlerteSeenByAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee emp = serviceEmployee.getByLoginUser(value);
            var alertes = service.GetAll();
            List<AlerteViewModel> a = new List<AlerteViewModel>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);

            foreach (var item in alertes)
            {
                AlerteViewModel test = new AlerteViewModel();
                test.titreAlerte = item.titreAlerte;
                test.description = item.description;
                if (item.senderId != null)
                {
                    Employee sender = serviceEmployee.getById(item.senderId);
                    test.senderId = item.senderId;
                    test.senderName = sender.userName;
                    test.status = item.status;
                    test.Id = item.Id;

                }
                if (item.reciverId != null)
                {
                    Employee reciver = serviceEmployee.getById(item.reciverId);
                    test.reciverName = reciver.userName;
                    test.reciverId = item.reciverId;
                    test.status = item.status;
                    test.Id = item.Id;
                }
                a.Add(test);
            }


            List<AlerteViewModel> recivedAlertes = a.Where(p => p.reciverId == emp.Id && p.status=="seen").ToList<AlerteViewModel>();
            a = a.Where(p => p.reciverId == emp.Id && p.status == "NotSeen").ToList<AlerteViewModel>();
            int b = 0;
            if (a != null)
            {
                b = a.Count;

            }
            else
            {
                a = null;
                b = 0;

            }
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                ViewBag.nb = b;
                ViewBag.alertes = a;
                return View(recivedAlertes);   //fVM.Take(10)
            }
        }
        [HttpGet]
        public ActionResult FindAlerte(string Id,string dateCreation)
        {


            Employee emp = serviceEmployee.getByLogin(Id);
            DateTime date = DateTime.Parse(dateCreation);
            List<Alerte> alertes = service.getByDate(date, 112);
            //String login = Session["loginIndex"].ToString();
            //Employee item = serviceEmployee.getByLoginUser(login);
            var a = new AlerteViewModel();
            var usersAssociees = serviceGroupeEmp.getListEmployeeByGroupe(104);
            var tests = usersAssociees.Select(o => o.userName).Distinct().ToList();
            foreach (String test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Value = test, Text = test });
            }

            if (Request.IsAjaxRequest())
            {
                return View(a);

            }
            else
            {
                return PartialView("_AlerteDajoutAlerte", a);
            }
        }
        public ActionResult UpdateAlerte(int? Id)
        {
            Alerte alerte = service.getById(Id);
            if (TryUpdateModel(alerte))
            {
                try
                {
                    alerte.status = "seen";
                    service.SaveChange();
                    return RedirectToAction("AlerteSeenByAgent");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Erreur!!!!!");
                }
            }
            return RedirectToAction("AlerteSeenByAgent");

        }
        public ActionResult ReponseAlerte(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Alerte alerte = service.getById(id);
            AlerteViewModel a = new AlerteViewModel();
            a.dateCreation = alerte.dateCreation;
            a.description = alerte.description;
            a.titreAlerte = alerte.titreAlerte;

            return View(a);
        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("ReponseAlerte")]
        public ActionResult ReponseAlerteTest(int? id,FormCollection form)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alerte = service.getById(id);
            string message = form["message"].ToString();
                    alerte.reponseAlerte = message;
                    service.SaveChange();
                    return RedirectToAction("IndexAgentManagerReciver");
                
           

        }

    }
}
