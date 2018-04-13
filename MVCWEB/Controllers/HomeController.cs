using Data;
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
        IEmployeeService serviceEmployees;
        IUserService serviceUser;
        IGroupeEmployeeService serviceGroupeEmp;
        IGroupeService serviceGroupe;
        IEventService serviceEvent;
        static int idEmpConnecte;
        public HomeController()
        {
            serviceEmployees = new EmployeeService();
            serviceUser = new UserService();
            serviceGroupeEmp = new GroupesEmployeService();
            serviceGroupe = new GroupeService();
            serviceEvent = new EventService();
        }
        [HttpGet]
        public ActionResult IndexManagerAgentTest(int id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
            if (empConnected.Content != null)
            {
                String strbase64 = Convert.ToBase64String(empConnected.Content);
                String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = empConnected.userName;
            ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;
            var employees = serviceEmployees.GetAll();
            List<Employee> emps = serviceGroupeEmp.getListEmployeeByGroupeId(id);
            User user = new User();
            List<EmployeeViewModel> fVM = new List<EmployeeViewModel>();
            List<SelectListItem> groupesassocies = new List<SelectListItem>();
            String Url = null;
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
            var employees = serviceEmployees.GetAll();
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
        public ActionResult IndexManagerGroupes(int? id)
        {

            string value = (string)Session["loginIndex"];
            //var employees = service.GetAll();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            idEmpConnecte = (int)id;
            Employee item = serviceEmployees.getById(id);
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
            Employee item = serviceEmployees.getById(Id);


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
        public ActionResult ManagerGroupes()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
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
        public ActionResult Calendar()
        {
            String login = Session["loginIndex"].ToString();
            Employee item = serviceEmployees.getByLoginUser(login);
            if (item.Content != null)
            {
                String strbase64 = Convert.ToBase64String(item.Content);
                String empConnectedImage = "data:" + item.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
                ViewBag.nameEmpConnected = item.userName;
                ViewBag.pseudoNameEmpConnected = item.pseudoName;
                ViewBag.IdEmpConnected = item.Id;
            }
            var groupesassociees = serviceGroupeEmp.getGroupeByIDEmployee(item.Id);
            var usersAssociees = serviceGroupeEmp.getListEmployeeByGroupe(item.Id);
            EventViewModel a = new EventViewModel();
            var logins = serviceUser.GetAll();
            var tests = usersAssociees.Select(o => o.userLogin).Distinct().ToList();
            foreach (String test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Text = test, Value = test });
            }
            var groupes = serviceGroupe.GetAll();
            var groupeslogins = groupesassociees.Select(o => o.nom).Distinct().ToList();
            foreach (var test in groupeslogins)
            {
                //foreach(var assoc in groupesassociees){
                //    if (!(test.nom).Equals(assoc.nom)){
                a.groupesass.Add(new SelectListItem { Text = test, Value = test });
                //}
            }
            using (ReportContext dc = new ReportContext())
            {
                var events = dc.events.ToList();
                List<Event> eventsTests = new List<Event>();
                List<DateTime> dateTimes = new List<DateTime>();
                List<String> titles = new List<String>();
                string[] arr1 = new string[400];
                foreach (var test in events)
                {
                    eventsTests.Add(test);
                    dateTimes.Add(test.dateDebut);
                    titles.Add(test.titre);
                    //arr1[1]=(test.titre);
                    //arr1[2] = (""+test.dateDebut);
                }
                ViewBag.events = eventsTests;
                ViewBag.titres = titles;
                ViewBag.dateTimes = dateTimes;
                ViewBag.arr1 = arr1;
            }
            return View(a);
            //using (ReportContext dc = new ReportContext())
            //{
            //    var events = dc.events.ToList();
            //    List<Event> eventsTests = new List<Event>();
            //    List<DateTime> dateTimes = new List<DateTime>();
            //    List<String> titles = new List<String>();

            //    foreach (var test in events)
            //    {
            //        eventsTests.Add(test);
            //        dateTimes.Add(test.dateDebut);
            //        titles.Add(test.titre);
            //    }
            //    ViewBag.titres = titles;
            //    ViewBag.dateTimes = dateTimes;
            //    return View(events);
            //}
        }
        public ActionResult GetEventsByImed()
        {
            String login = Session["loginIndex"].ToString();
            Employee item = serviceEmployees.getByLoginUser(login);
            List<Event> eventsTestsAll = new List<Event>();
            List<Employee> emps = serviceGroupeEmp.getListEmployeeByGroupe(item.Id);
            var testsEmps = emps.Distinct().ToList();
            List<Groupe> groupes = serviceGroupeEmp.getGroupeByIDEmployee(item.Id);
            List<Event> groupesEvents = serviceEvent.getListEventsByListGroupes(groupes);
            var groupesEventsDistinct = groupesEvents.Distinct().ToList();
            foreach (var groupeEvent in groupesEventsDistinct)
            {
                //groupeEvent.start = groupes[0].nom;
                eventsTestsAll.Add(groupeEvent);
            }
            foreach (var employee in testsEmps)
            {
                var eventss = serviceEvent.getListEventsByEmployeeId(employee.Id);
                foreach (var eventt in eventss)
                {
                    eventsTestsAll.Add(eventt);
                }
            }
            var eventsTests = serviceEvent.GetAll();
            var eventsAll = new List<EventViewModel>();
            foreach (var test in eventsTestsAll.Distinct())
            {
                Employee emp = serviceEmployees.getById(test.employeeId);
                EventViewModel a = new EventViewModel();
                a.Id = test.Id;
                a.start = test.start;
                a.end = test.end;
                a.dateDebut = test.dateDebut;
                a.dateFin = test.dateFin;
                a.themeColor = test.themeColor;
                if (emp != null)
                {
                    a.employeeName = emp.userName;
                }
                a.description = test.description;
                if (test.titre.Equals("Congé") || (test.titre.Equals("Autorisation")))
                {
                    a.titre = test.titre + " " + emp.userName;
                }
                else
                {
                    a.titre = test.titre + " " + test.start;
                }
                eventsAll.Add(a);
            }
            var eventList = from e in eventsAll

                            select new
                            {

                                id = e.Id,

                                title = e.titre,

                                start = e.dateDebut.ToString("s"),

                                end = e.dateFin.ToString("s"),
                                //end = e.dateFin.AddDays(1).ToString("s"),


                                color = e.themeColor,

                                allDay = false

                            };
            var events = eventList.ToArray();

            return Json(events, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEvents(double start, double end)

        {
            var events = serviceEvent.GetAll();
            //List<Event> fVM = new List<Event>();
            //foreach (var test in events)
            //{
            //    fVM.Add(test);
            //}


            var fromDate = ConvertFromUnixTimestamp(start);

            var toDate = ConvertFromUnixTimestamp(end);




            var eventList = from e in events

                            select new
                            {

                                id = e.Id,

                                title = e.titre,

                                start = e.dateDebut.ToString("s") + " " + e.start + ":00",

                                end = e.dateFin.ToString("s") + " " + e.end + ":00",

                                color = e.themeColor,

                                allDay = false

                            };

            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);

        }
        private static DateTime ConvertFromUnixTimestamp(double timestamp)

        {

            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return origin.AddSeconds(timestamp);

        }

        public ActionResult FindEvent(int? Id)
        {
            Event eventt = serviceEvent.getById(Id);
            Employee item = serviceEmployees.getById(eventt.employeeId);


            var a = new EventViewModel();
            a.Id = eventt.Id;
            a.description = eventt.description;
            a.dateDebut = eventt.dateDebut;
            a.dateFin = eventt.dateFin;
            if (item != null)
            {
                a.employeeName = item.userName;
            }
            if (eventt.titre.Equals("Congé") || (eventt.titre.Equals("Autorisation")))
            {
                a.titre = eventt.titre + " " + item.userName;
            }
            else
            {
                a.titre = eventt.titre;
            }


            if (Request.IsAjaxRequest())
            {
                return PartialView("_AlerteDetailsEvent", a);
            }

            else
            {
                return View(a);
            }
        }
        public ActionResult DeleteEvent(int? id)
        {

            Event eventt = serviceEvent.getById(id);

            serviceEvent.Delete(eventt);
            serviceEvent.SaveChange();
            return RedirectToAction("Calendar");
        }
        public ActionResult Messages(int? id)
        {
            return View();
        }
        public ActionResult ManagerJournalierAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
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
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
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
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
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
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
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
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
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

        public ActionResult ManagerJournalierActivity()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("JournalierActivity", "IndicateursActivity", new { @id = IdEmpConnected });
            }
        }
        // Fin Controllers in Manager Template

        //Controllers in Agent Template   
        public ActionResult JournalierAgent()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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

                serviceEmployees = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = serviceEmployees.getById(id);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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

                serviceEmployees = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = serviceEmployees.getById(id);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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

                serviceEmployees = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = serviceEmployees.getById(id);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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

                serviceEmployees = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = serviceEmployees.getById(id);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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

                serviceEmployees = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = serviceEmployees.getById(id);
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
            Employee empConnected = serviceEmployees.getByLoginUser(value);
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

                serviceEmployees = new EmployeeService();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee emp = serviceEmployees.getById(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }
        }
       
        public ActionResult ManagerHebdoActivity()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("HebdoActivity", "IndicateursActivity", new { @id = IdEmpConnected });
            }
        }
        public ActionResult ManagerMensuelActivity()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("MensuelActivity", "IndicateursActivity", new { @id = IdEmpConnected });
            }
        }
        public ActionResult ManagerTrimestrielActivity()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("TrimestrielActivity", "IndicateursActivity", new { @id = IdEmpConnected });
            }
        }
        public ActionResult ManagerAnnuelleActivity()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getById(idEmpConnecte);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("AnnuelleActivity", "IndicateursActivity", new { @id = IdEmpConnected });
            }
        }
        public ActionResult IndexAgentGroupes()
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = serviceEmployees.getByLoginUser(value);
            if (value == null)
            {
                ViewBag.message = ("session cleared!");
                ViewBag.color = "red";
                return View("~/Views/Authentification/Index.cshtml");
            }
            else
            {
                int IdEmpConnected = empConnected.Id;
                return RedirectToAction("IndexAgentGroupes", "IndicateursActivity", new { @id = IdEmpConnected });
            }
        }
    }
}
