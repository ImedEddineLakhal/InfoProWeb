using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using Domain.Entity;
using Services;


namespace MVCWEB.Controllers
{
    public class UserController : Controller
    {

        IUserService service;
        public UserController()
        {

            service = new UserService();
        }
        // GET: User
        public ActionResult Index(String search, FormCollection form)
        {
            var users = service.GetAll();
            List<User> fVM = new List<User>();


            foreach (var item in users)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.nomPrenom.ToLower().Contains(search.ToLower())).ToList<User>();


            }
            return View(fVM);   //fVM.Take(10)
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = service.getById(id);
            return View(user);
        }

        // GET: User/Create

        // POST: User/Create


        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = service.getById(id);

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = service.getById(id);

            if (TryUpdateModel(user))
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
            return View(user);

        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            User user = service.getById(id);

            service.Delete(user);
            service.SaveChange();
            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
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
        public ActionResult FindUser(int? Id)
        {
                User item = service.getById(Id);


           // var a = new EmployeeViewModel();
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
