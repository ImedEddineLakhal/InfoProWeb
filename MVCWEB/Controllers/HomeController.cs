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
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
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


          
                return View(fVM);   //fVM.Take(10)
            }

        }
        [HttpGet]
        public ActionResult IndexManagerAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            idEmpConnecte = (int)id;
          //  Employee item = service.getById(id);
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
                //  return View(fVM);   //fVM.Take(10)
                return RedirectToAction("JournalierAgent", "Home", new { @id = idEmpConnecte });
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
        
             public ActionResult ManagerGroupes()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("IndexManagerGroupes", "Home", new { @id = IdEmpConnected });
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
        public ActionResult TestChart(int? id)
        {
         
            return View();
        }

        //controllers for Manager Template 
        public ActionResult ManagerJournalierAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("Index", "Indicateurs", new { @id = IdEmpConnected });
            }
        }

        public ActionResult ManagerHebdoAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("Hebdo", "Indicateurs", new { @id = IdEmpConnected });
            }
        }

        public ActionResult ManagerMensuelAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("Mensuel", "Indicateurs", new { @id = IdEmpConnected });
            }
        }

        public ActionResult ManagerTrimestrielleAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("Trimestriel", "Indicateurs", new { @id = IdEmpConnected });
            }
        }

        public ActionResult ManagerAnnuelleAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("Annuelle", "Indicateurs", new { @id = IdEmpConnected });
            }
        }
        // Fin Controllers in Manager Template

        //Controllers in Agent Template   
        public ActionResult JournalierAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("JournalierAgent", "Indicateurs", new { @id = IdEmpConnected });
            }
        }
        public ActionResult HebdoAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("HebdoAgent", "Indicateurs", new { @id = IdEmpConnected });
            }
        }
        public ActionResult MensuelAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("MensuelAgent", "Indicateurs", new { @id = IdEmpConnected });
            }
        }
        public ActionResult TrimestrielAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("TrimestrielAgent", "Indicateurs", new { @id = IdEmpConnected });
            }
        }
        public ActionResult AnnuelleAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("AnnuelleAgent", "Indicateurs", new { @id = IdEmpConnected });
            }
        }
        //Fin Controllers in Agent Template 

        // Controllers in Manager Template for selected agent
        public ActionResult JournalierSelectedAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                if (empConnected.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(empConnected.Content);
                    String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                    ViewBag.empConnectedImage = empConnectedImage;
                }
                ViewBag.nameEmpConnected = empConnected.userName;
                ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

                service = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = service.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }

        public ActionResult JournalierSelectedAgent1(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                if (empConnected.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(empConnected.Content);
                    String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                    ViewBag.empConnectedImage = empConnectedImage;
                }
                ViewBag.nameEmpConnected = empConnected.userName;
                ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

                service = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = service.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }
        public ActionResult HebdoSelectedAgent(int? id)
        {
            var semaines = new List<SelectListItem>();
            for (int m = 1; m <= 52; m++)
            {
                var val = m.ToString();

                semaines.Add(new SelectListItem { Text = "Semaine" + val, Value = val });
            }
            ViewBag.SemaineItems = semaines;
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                if (empConnected.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(empConnected.Content);
                    String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                    ViewBag.empConnectedImage = empConnectedImage;
                }
                ViewBag.nameEmpConnected = empConnected.userName;
                ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

                service = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = service.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }
        public ActionResult MensuelSelectedAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                if (empConnected.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(empConnected.Content);
                    String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                    ViewBag.empConnectedImage = empConnectedImage;
                }
                ViewBag.nameEmpConnected = empConnected.userName;
                ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

                service = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = service.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }
        public ActionResult TrimestrielSelectedAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                if (empConnected.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(empConnected.Content);
                    String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                    ViewBag.empConnectedImage = empConnectedImage;
                }
                ViewBag.nameEmpConnected = empConnected.userName;
                ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

                service = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = service.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }
        public ActionResult AnnuelleSelectedAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                if (empConnected.Content != null)
                {
                    String strbase64 = Convert.ToBase64String(empConnected.Content);
                    String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                    ViewBag.empConnectedImage = empConnectedImage;
                }
                ViewBag.nameEmpConnected = empConnected.userName;
                ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

                service = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = service.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }
        // Fin Controllers in Manager Template for selected agent
    }
}