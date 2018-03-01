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
    public class EmployeeController : Controller
    {
        // GET: Employee
        IEmployeeService service;
        public EmployeeController()
        {

            service = new EmployeeService();
        }
        public ActionResult Index(String search, FormCollection form)
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

                fVM = fVM.Where(p => p.login.ToLower().Contains(search.ToLower())).ToList<Employee>();


            }
            return View(fVM);   //fVM.Take(10)
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee employee = service.getById(id);
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var employee = new Employee();
            return View(employee);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee c, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            Employee employee = new Employee
            {
                Id = c.Id,
                login = c.login,
                password = c.password,
                role = c.role

            };
            service.Add(employee);
            service.SaveChange();


            return RedirectToAction("Index");

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee employee = service.getById(id);

            return View(employee);
        }

        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = service.getById(id);

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

            Employee employee = service.getById(id);

            service.Delete(employee);
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
