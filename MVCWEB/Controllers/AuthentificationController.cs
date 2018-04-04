using MVCWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using Domain.Entity;
using Services;
using System.Data;

namespace MVCWEB.Controllers
{
    public class AuthentificationController : Controller
    {
        // GET: Authentification
        IUserService service;
        IEmployeeService serviceEmployee;
        IGroupeEmployeeService serviceGroupeEmp;
        public static String loginIndex;
        public AuthentificationController()
        {

            service = new UserService();
            serviceEmployee = new EmployeeService();
            serviceGroupeEmp = new GroupesEmployeService();
        }
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        // GET: Authentification/Details/5

        public ActionResult Connect(UserAuthentif userAuthe)
        {
            string login = userAuthe.login;
            string password = userAuthe.password;
            Session["loginIndex"] = login;

            if (password == null)
            {
                ViewBag.message = ("(*)Champ mot de passe obligatoire!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                try
                {
                    DirectoryEntry Ldap = new DirectoryEntry("LDAP://info.local", login, password);
                    DirectorySearcher searcher = new DirectorySearcher(Ldap);
                    searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + login + "))";
                    SearchResult result = searcher.FindOne();
                    DirectoryEntry DirEntry = result.GetDirectoryEntry();
                    /*if (result != null)
                    {
                        Session["nomPrenom"] = (string)result.Properties["cn"][0];
                        int groupCount = result.Properties["memberOf"].Count;
                       /* for (int counter = 0; counter < groupCount; counter++)
                        {
                            groupsList.Append(DirEntry.Properties["memberOf"][counter]);
                            groupsList.Append("|");
                            msg = groupsList.ToString().Split('|');
                            msg2 = msg[counter].Split(',');
                            tab[counter] = msg2[0];
                            Session["groupes"] = tab[0];
                        }*/
                    // }
                }
                catch (Exception)
                {
                    ViewBag.message = ("login ou mot de passe incorrect!");
                    ViewBag.color = "red";
                    return View("~/Views/Authentification/Index.cshtml");
                }
                Employee emp = serviceEmployee.getByLoginUser(login);
                if (emp != null)
                {
                    if ((emp.role).Equals("Manager"))
                    {

                        User user = new User();
                        user.login = login;
                        user.logEntree = DateTime.Now;
                        //user.logSortie =new DateTime();
                        service.Add(user);
                        service.SaveChange();
                        return RedirectToAction("IndexManagerGroupes", "Home",new { @id = emp.Id } );
                    }
                    else if ((emp.role).Equals("Agent"))
                    {

                        User user = new User();
                        user.login = login;
                        user.logEntree = DateTime.Now;
                        //user.logSortie =new DateTime();
                        service.Add(user);
                        service.SaveChange();

                        return RedirectToAction("JournalierAgent", "Indicateurs", new { @id = emp.Id });
                    }
                    else if ((emp.role).Equals("Admin"))
                    {
                        User user = new User();
                        user.login = login;
                        user.logEntree = DateTime.Now;
                        //user.logSortie =new DateTime();
                        service.Add(user);
                        service.SaveChange();

                        return View("~/Views/Home/Index.cshtml");
                    }
                    else return View("~/Views/Authentification/Index.cshtml");
                }
            }
            return View("~/Views/Authentification/Index.cshtml");
        }

        // GET: Authentification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authentification/Create
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

        // GET: Authentification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authentification/Edit/5
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

        // GET: Authentification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authentification/Delete/5
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
        public ActionResult logout()
        {
            User user = service.getByTempSortie((string)Session["loginIndex"]);
            user.logSortie = DateTime.Now;
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
            return RedirectToAction("Index");
        }
    }
}