using Domain.Entity;
using MVCWEB.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCWEB.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeService service;
        IUserService serviceUser;
        IGroupeEmployeeService serviceGroupeEmp;
        static int idEmpConnecte;
        public HomeController()
        {
            service = new EmployeeService();
            serviceUser = new UserService();
            serviceGroupeEmp = new GroupesEmployeService();

        }
        [HttpGet]
        public ActionResult IndexManagerAgentTest(int id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (empConnected.Content != null)
            {
                String strbase64 = Convert.ToBase64String(empConnected.Content);
                String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = empConnected.userName;
            ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;
            var employees = service.GetAll();
            List<Employee> emps = serviceGroupeEmp.getListEmployeeByGroupeId(id);
            User user = new User();
            List<EmployeeViewModel> fVM = new List<EmployeeViewModel>();
            List<SelectListItem> groupesassocies = new List<SelectListItem>();
            String Url=null;
            foreach (var item in emps)
            {
                if (item.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(item.Content);
                     Url = "data:" + item.ContentType + ";base64," + strbase64;
                     ViewBag.url = Url;
                }
                if (item.userId != null)
                {
                    if (item.Id != empConnected.Id)
                    {
                        fVM.Add(
                          new EmployeeViewModel
                          {
                              Id = item.Id,
                              userName = item.userName,
                              pseudoName = item.pseudoName,
                              userLogin = item.userLogin,
                              Activite = item.Activite,
                              IdHermes = item.IdHermes,
                              image = Url,
                              role = item.role,

                              //groupesassocies=groupesassocies



                          });
                    }
                }
                else
                {
                    user = null;
                    if (item.Id != empConnected.Id)
                    {
                        fVM.Add(
                  new EmployeeViewModel
                  {
                      Id = item.Id,
                      userName = item.userName,
                      pseudoName = item.pseudoName,
                      userLogin = "",
                      Activite = item.Activite,
                      IdHermes = item.IdHermes,
                      role = item.role,
                      image = Url
                      //groupesassocies = groupesassocies


                  });
                    }
                }
                //groupesassocies.Clear();


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
        public ActionResult IndexManagerAgent()
        {
            string value = (string)Session["loginIndex"];
            var employees = service.GetAll();
            User user = new User();
            List<EmployeeViewModel> fVM = new List<EmployeeViewModel>();
            List<SelectListItem> groupesassocies = new List<SelectListItem>();

            foreach (var item in employees)
            {
                if (item.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(item.Content);
                    String Url = "data:" + item.ContentType + ";base64," + strbase64;
                    ViewBag.url = Url;
                }
                if (item.userId != null)
                {
                    user = serviceUser.getById(item.userId);

                    fVM.Add(
                      new EmployeeViewModel
                      {
                          Id = item.Id,
                          userName = item.userName,
                          pseudoName = item.pseudoName,
                          userLogin = item.userLogin,
                          Activite = item.Activite,
                          IdHermes = item.IdHermes,
                          role = item.role
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
                      userLogin = "",
                      Activite = item.Activite,
                      IdHermes = item.IdHermes,
                      role = item.role
                      //groupesassocies = groupesassocies


                  });
                }
                //groupesassocies.Clear();


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
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult IndexManagerGroupes(int ? id)
        {

            string value = (string)Session["loginIndex"];
            //var employees = service.GetAll();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            idEmpConnecte =(int) id;
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
            var tests = logins.Select(o => o.login).Distinct().ToList();
            foreach (var test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Text = test, Value = test });
            }
           
            var groupesassociees = serviceGroupeEmp.getGroupeByIDEmployee(item.Id);
            var groupesassociees_tests = groupesassociees.Select(o => o.nom).Distinct().ToList();
            a.Group = new List<Groupe>();
            foreach (var test in groupesassociees)
            {
                a.Group.Add(test);
            }
            if (item.Content != null)
            {
                String strbase64 = Convert.ToBase64String(item.Content);
                String Url = "data:" + item.ContentType + ";base64," + strbase64;
                ViewBag.url = Url;
                a.image = Url;
            }
            
            //  a.PhotoUrl = airflight.PhotoUrl;
            if (item == null)
                return HttpNotFound();




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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
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
                return PartialView("_AlerteStatAttendance", a);
            }

            else
            {
                return View(a);
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TestChart()
        {

            return View();
        }
    }
}