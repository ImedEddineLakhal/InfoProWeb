using Domain.Entity;
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
    public class EmployeeController : Controller
    {
        // GET: Employee
        IEmployeeService service;
        IUserService serviceUser;
        public EmployeeController()
        {

            service = new EmployeeService();
            serviceUser = new UserService();
        }
        public ActionResult Index(String search, FormCollection form,int? CallsToMake)
        {
            var employees = service.GetAll();
            User user = new User();
            List<EmployeeViewModel> fVM = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                if (item.userId != null) { 
                user = serviceUser.getById(item.userId);
                    fVM.Add(
                      new EmployeeViewModel
                      {
                          Id = item.Id,
                          userName = item.userName,
                          pseudoName = item.pseudoName,
                          userLogin = user.login,
                          Activite=item.Activite,
                          IdHermes=item.IdHermes,
                          role=item.role

                      });
                }
                else
                {
                    user = null;
                    fVM.Add(
                  new EmployeeViewModel
                  {
                      Id = item.Id,
                      userName = item.userName,
                      pseudoName = item.pseudoName,
                      userLogin ="",
                      Activite = item.Activite,
                      IdHermes = item.IdHermes,
                      role = item.role

                  });
                }
              
            }

            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.userName.ToLower().Contains(search.ToLower())).ToList<EmployeeViewModel>();

            }
            return View(fVM);
        }
        [HttpGet]
        public ActionResult RefreshTable(String search, FormCollection form, int? CallsToMake)
        {
            var employees = service.GetAll();
            List<Employee> fVM = new List<Employee>();
            //string type = form["test"].ToString();
            //int numVal = Int32.Parse(type);
            foreach (var item in employees)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.userName.ToLower().Contains(search.ToLower())).ToList<Employee>();


            }

            return View(fVM);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            Employee item = service.getById(id);
            var a = new EmployeeViewModel();
            a.Id = item.Id;
            a.userName = item.userName;
            a.pseudoName = item.pseudoName;
            a.IdAD = (int)item.userId;
            a.IdHermes = item.IdHermes;
            a.Activite = item.Activite;
            a.role = item.role;
            //  a.PhotoUrl = airflight.PhotoUrl;
            if (item == null)
                return HttpNotFound();
            return View(a);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var employee = new EmployeeViewModel();
            var logins = serviceUser.GetAll();
            foreach (var test in logins)
            {
                employee.utilisateurs.Add(new SelectListItem { Text = test.login, Value = test.login });
            }
            return View(employee);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel item, FormCollection form, String utilisateur)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            var usr = serviceUser.getBylogin(utilisateur);
            Employee emp = new Employee
            {
                Id = item.Id,
                userName = item.userName,
                pseudoName = item.pseudoName,
                userId=usr.Id,
                Activite=item.Activite,
                IdHermes=item.IdHermes,
                role=item.role
                

            };
            // TODO: Add insert logic here

            service.Add(emp);
            service.SaveChange();

            return RedirectToAction("Index");

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id, FormCollection form)
                {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee item = service.getById(id);
            var a = new EmployeeViewModel();
            a.Id = item.Id;
            a.userName = item.userName;
            a.pseudoName = item.pseudoName;
            a.IdAD = (int)item.userId;
            a.IdHermes = item.IdHermes;
            a.Activite = item.Activite;
            a.role = item.role;
            //string type = form["typeGenerator"].ToString();
            var logins = serviceUser.GetAll();
            foreach (var test in logins)
            {
                a.utilisateurs.Add(new SelectListItem { Text=test.login,Value=test.login});
            }
            //  a.PhotoUrl = airflight.PhotoUrl;
            if (item == null)
                return HttpNotFound();
            return View(a);
        

        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditEmployee(int? id,String utilisateur)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = service.getById(id);
            var usr = serviceUser.getBylogin(utilisateur);
            employee.userId = usr.Id;
            if (TryUpdateModel(employee))
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
            return View(employee);

        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee emp = new Employee();
            emp = service.getById(id);
            EmployeeViewModel a = new EmployeeViewModel();
            a.Id = emp.Id;
            service.Delete(emp);

            service.SaveChange();
            if (emp == null)
                return HttpNotFound();
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
        public ActionResult FindEmployee(int? Id)
        {
            Employee item = service.getById(Id);


            var a = new EmployeeViewModel();
            a.Id = item.Id;
            a.userName = item.userName;
            a.pseudoName = item.pseudoName;
            a.IdAD = (int)item.userId;
            a.IdHermes = item.IdHermes;
            a.Activite = item.Activite;
            a.role = item.role;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AlerteDeSuppression", a);
            }

            else
            {
                return View(a);
            }
        }
    }
}
