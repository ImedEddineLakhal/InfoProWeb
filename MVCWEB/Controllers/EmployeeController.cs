using Data;
using Domain.Entity;
using MVCWEB.Models;
using MyFinance.Data.Infrastructure;
using MyReports.Data.Infrastructure;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCWEB.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        IEmployeeService service;
        IUserService serviceUser;
        IGroupeService serviceGroupe;
        ReportContext reportContext;
        IDatabaseFactory dbFactory;
        IUnitOfWork uow;
        IGroupeEmployeeService servicegroupemp;
        static int Idtest;
        static String[] ps;
        public EmployeeController()
        {

            service = new EmployeeService();
            serviceUser = new UserService();
            serviceGroupe = new GroupeService();
            reportContext = new ReportContext();
            dbFactory = new DatabaseFactory();
            uow = new UnitOfWork(dbFactory);
            servicegroupemp = new GroupesEmployeService();

        }
        public ActionResult Index(String search, FormCollection form,int? CallsToMake)
        {
            string value = (string)Session["loginIndex"];
            var employees = service.GetAll();
            User user = new User();
            List<EmployeeViewModel> fVM = new List<EmployeeViewModel>();
            List<SelectListItem> groupesassocies = new List<SelectListItem>();

            foreach (var item in employees)
            {
                if (item.userId != null) { 
                    user = serviceUser.getById(item.userId);
                    //var groupesassociees = servicegroupemp.getGroupeByIDEmployee(item.Id);
                    //var groupesassociees_tests = groupesassociees.Select(o => o.nom).Distinct().ToList();
                   
                    //    foreach (var test in groupesassociees_tests)
                    //    {
                    //       groupesassocies.Add(new SelectListItem { Text = test, Value = test });
                    //    }
                    
                    fVM.Add(
                      new EmployeeViewModel
                      {
                          Id = item.Id,
                          userName = item.userName,
                          pseudoName = item.pseudoName,
                          userLogin = item.userLogin,
                          Activite=item.Activite,
                          IdHermes=item.IdHermes,
                          role=item.role
                          //groupesassocies=groupesassocies



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
                      //groupesassocies = groupesassocies


                  });
                }
                //groupesassocies.Clear();


            }
           
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.userName.ToLower().Contains(search.ToLower())).ToList<EmployeeViewModel>();

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
            List<Groupe> gremp = servicegroupemp.getGroupeByIDEmployee(id);
            var tests = gremp.Select(o => o.nom).Distinct().ToList();
            List<EmployeeViewModel> fVM = new List<EmployeeViewModel>();
            List<SelectListItem> groupes = new List<SelectListItem>();
            User user = new User();
            Employee item = service.getById(id);
            var a = new EmployeeViewModel();
            a.Id = item.Id;
            a.userName = item.userName;
            a.pseudoName = item.pseudoName;
            a.IdAD = (int)item.userId;
            a.IdHermes = item.IdHermes;
            a.Activite = item.Activite;
            a.role = item.role;
            user = serviceUser.getById(item.userId);
            a.userLogin = item.userLogin;
            foreach (var test in tests)
            {
                groupes.Add(new SelectListItem { Text = test, Value = test });
            }
            a.groupes = groupes;
            if (item.Content != null)
            {
                String strbase64 = Convert.ToBase64String(item.Content);
                String Url = "data:" + item.ContentType + ";base64," + strbase64;

                ViewBag.url = Url;
            }
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
            var tests= logins.Select(o => o.login).Distinct().ToList();
            foreach (var test in tests)
            {
                employee.utilisateurs.Add(new SelectListItem { Text = test, Value = test });
            }
            var groupes = serviceGroupe.GetAll();
            foreach (var test in groupes)
            {
                employee.groupes.Add(new SelectListItem { Text = test.nom, Value = test.nom });
            }
            return View(employee);
        }

        // POST: Medcin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel item, FormCollection form, String utilisateur,String groupes, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create");
            }
            string type = form["typeGenerator"].ToString();
            //var path = Path.GetFullPath(file.FileName);
            var fileName = Path.GetFileName(file.FileName);
            string contenttype = String.Empty;
            FileStream fs1 = new FileStream("C:\\images\\" + fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs1);
            Byte[] bytes = br.ReadBytes((Int32)fs1.Length);
            var usr = serviceUser.getBylogin(utilisateur);
            if (!utilisateur.Equals(""))
            {
                Employee emp = new Employee
                {
                    Id = item.Id,
                    userName = item.userName,
                    pseudoName = item.pseudoName,
                    userId = usr.Id,
                    Activite = item.Activite,
                    IdHermes = item.IdHermes,
                    role = item.role,
                    userLogin=item.userLogin,
                    ContentType = type,
                    Content = bytes

                };
                service.Add(emp);
                service.SaveChange();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Create");

            //var group = serviceGroupe.getByNom(groupes);

            //List<Groupe> testss = new List<Groupe>();
            //emp.Group = testss;
            //emp.Group.Add(group);
            //service.SaveChange();


        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
                {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee item = service.getById(id);
            Idtest = item.Id;
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
            var tests = logins.Select(o => o.login).Distinct().ToList();
            foreach (var test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Text=test,Value=test});
            }
            var groupes = serviceGroupe.GetAll();
            foreach (var test in groupes)
            {

                a.groupes.Add(new SelectListItem { Text = test.nom, Value = test.nom });
                //a.GroupeTests.Add(test);
            }
            var groupesassociees = servicegroupemp.getGroupeByIDEmployee(item.Id);
            var groupesassociees_tests = groupesassociees.Select(o => o.nom).Distinct().ToList();
            foreach (var test in groupesassociees_tests)
            {
                a.groupesassocies.Add(new SelectListItem { Text = test, Value = test });
            }
            //  a.PhotoUrl = airflight.PhotoUrl;
            if (item == null)
                return HttpNotFound();
            return View(a);
        

        }
        public ActionResult EditTest(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee item = service.getById(id);
            Idtest = item.Id;
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
            var tests = logins.Select(o => o.login).Distinct().ToList();
            foreach (var test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Text = test, Value = test });
            }
            var groupes = serviceGroupe.GetAll();
            var groupesassociees = servicegroupemp.getGroupeByIDEmployee(item.Id);
            var groupesassociees_tests = groupesassociees.Select(o => o.nom).Distinct().ToList();
            foreach (var test in groupes)
            {
                //foreach(var assoc in groupesassociees){
                //    if (!(test.nom).Equals(assoc.nom)){
                        a.groupes.Add(new SelectListItem { Text = test.nom, Value = test.nom });
                    //}
                }
                //a.GroupeTests.Add(test);
            
            
            foreach (var test in groupesassociees_tests)
            {
                a.groupesassocies.Add(new SelectListItem { Text = test, Value = test });
            }
            //  a.PhotoUrl = airflight.PhotoUrl;
            if (item == null)
                return HttpNotFound();
            return View(a);


        }
        // POST: Medcin/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditEmployee(int? id,String utilisateur, FormCollection form, HttpPostedFileBase file)
        {
            List<string> objs = new List<string>();
            //objs = tests.Select(p => p.ToString()).ToList();
         
            //string combindedString = string.Join(",", objs.ToArray());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = service.getById(id);
            if (utilisateur==null)
            {
                var usr = serviceUser.getById(employee.userId);
                employee.userId = usr.Id;
                if (objs != null)
                {

                    foreach (var value in ps)
                    {

                        Groupe groupe = serviceGroupe.getByNom((value));

                        GroupesEmployees gremp = new GroupesEmployees();
                        gremp.employeeId = employee.Id;
                        gremp.groupeId = groupe.Id;
                        servicegroupemp.Add(gremp);
                        servicegroupemp.SaveChange();


                    }
                }
            }

            else if (!utilisateur.Equals(""))
            {
                var usr = serviceUser.getBylogin(utilisateur);
                employee.userId = usr.Id;
                employee.userLogin = usr.login;
                if (ps != null)
                {

                    foreach (var value in ps)
                    {

                        Groupe groupe = serviceGroupe.getByNom((value));

                        GroupesEmployees gremp = new GroupesEmployees();
                        gremp.employeeId = employee.Id;
                        gremp.groupeId = groupe.Id;
                        servicegroupemp.Add(gremp);
                        servicegroupemp.SaveChange();


                    }
                }
            }
            //string type = form["typeGenerator"].ToString();
            //var path = Path.GetFullPath(file.FileName);
            //var fileName = Path.GetFileName(file.FileName);
            //string contenttype = String.Empty;
            //FileStream fs1 = new FileStream("C:\\images\\" + fileName, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs1);
            //Byte[] bytes = br.ReadBytes((Int32)fs1.Length);
            //employee.ContentType = type;
            //employee.Content = bytes;
            //Groupe groupe = serviceGroupe.getByNom(groupes);
            //ReportContext contextReport = new ReportContext();
            //var employee = contextReport.employees.Find(id);
            //var usr = uow.UserRepository.getByLogin(utilisateur);
            //var group = contextReport.groupes.Find(groupe.Id);

            //employee.Group.Add(group);
            //employee.Group.Add(group);
            if (TryUpdateModel(employee))
            {
                try
                {

                    service.SaveChange();
                    return RedirectToAction("Details", "Employee", new { @id = id });
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Erreur!!!!!");
                }
            }
            return View(employee);

        }
        [HttpPost, ActionName("EditTest")]
        public ActionResult EditEmployeeTest(int? id, String utilisateur, FormCollection form, HttpPostedFileBase file)
        {
            List<string> objs = new List<string>();
            //objs = tests.Select(p => p.ToString()).ToList();

            //string combindedString = string.Join(",", objs.ToArray());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = service.getById(id);
            
            string type = form["typeGenerator"].ToString();
            //var path = Path.GetFullPath(file.FileName);
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                string contenttype = String.Empty;
                FileStream fs1 = new FileStream("C:\\images\\" + fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs1);
                Byte[] bytes = br.ReadBytes((Int32)fs1.Length);
                employee.ContentType = type;
                employee.Content = bytes;
            }
            //Groupe groupe = serviceGroupe.getByNom(groupes);
            //ReportContext contextReport = new ReportContext();
            //var employee = contextReport.employees.Find(id);
            //var usr = uow.UserRepository.getByLogin(utilisateur);
            //var group = contextReport.groupes.Find(groupe.Id);

            //employee.Group.Add(group);
            //employee.Group.Add(group);
            if (TryUpdateModel(employee))
            {
                try
                {

                    service.SaveChange();
                    return RedirectToAction("Details", "Employee", new { @id = id });
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Erreur!!!!!");
                }
            }
            return View(employee);

        }
        static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                //builder.Append('.');
            }
            return builder.ToString();
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
        public ActionResult AffectationUserError()
        {
            EmployeeViewModel a = new EmployeeViewModel();
            return PartialView("_AffectationUserError",a);
        }
        [HttpGet]
        public ActionResult FindGroupesAssociees(int Id, String utilisateurs, String[] groupess, String str)
        {
           
            Employee item = service.getById(Id);
            var usr = new User() ;
            if (!utilisateurs.Equals(""))
            {
                 usr = serviceUser.getBylogin(utilisateurs);
                item.userId = usr.Id;

            }
            var a = new EmployeeViewModel();
            a.Id = item.Id;
            a.userName = item.userName;
            a.pseudoName = item.pseudoName;
            a.IdAD = (int)item.userId;
            a.IdHermes = item.IdHermes;
            a.Activite = item.Activite;
            a.role = item.role;
            a.userLogin=usr.login;
            ps = groupess;
            if (groupess!= null)
            {
                foreach (var test in groupess)
                {
                    //String test1 = Json(test.VA).ToString();

                    a.groupesassocies.Add(new SelectListItem { Text = test, Value = test });
                    a.tests.Add(test);
                }
                ViewBag.tests = a.groupesassocies;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AlerteAjoutsGroupes", a);
            }
            else
            {
                return View(a);
            }
        }
        [HttpGet]
        public ActionResult FindAffectationEmployee(int? Id)
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
                return PartialView("_AffectationUserError", a);
            }

            else
            {
                return View(a);
            }
        }
     
        //[HttpGet]
        //public ActionResult FindGroupeEmployee(int? Id)
        //{
        //    GroupesEmployees a = new GroupesEmployees();
        //    var groupesassociees = servicegroupemp.getByIDEmployee(Id);
        //    a.employeeId = groupesassociees[1].employeeId;
        //    a.groupeId = groupesassociees[1].groupeId;
        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_AlerteDeSuppressionGroupe", a);
        //    }

        //    else
        //    {
        //        return View(a);
        //    }
        //}
        //[HttpPost]
        //public ActionResult DeleteGrooupeEmployee()
        //{

        //    return View();
        //}
        public ActionResult deleteGrooupeEmployee(int? id,EmployeeViewModel model,String nom)
        {
            //String groupeName = TempData["name"].ToString();
            servicegroupemp.deletegroupeEmployeeByName(Idtest, nom);
            servicegroupemp.SaveChange();
            //TempData["name"] = null;
            return RedirectToAction("Edit","Employee",new { @id = Idtest });
        }

        //public ActionResult deleteGrooupeEmployee(int id, FormCollection collection)
        //{
        //    try
        //    {
        //    TODO: Add delete logic here

        //        return RedirectToAction("Edit");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        
    }
}
