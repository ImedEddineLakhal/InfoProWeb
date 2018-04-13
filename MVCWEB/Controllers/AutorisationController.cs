using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Entity;
using Services;
using MVCWEB.Models;

namespace MVCWEB.Controllers
{
    public class AutorisationController : Controller
    {
        IEventService service;
        IGroupeService serviceGroupe;
        IUserService serviceUser;
        IEmployeeService serviceEmployees;
        IGroupeEmployeeService serviceGroupeEmp;
        public AutorisationController()
        {
            service = new EventService();
            serviceGroupe = new GroupeService();
            serviceUser = new UserService();
            serviceEmployees = new EmployeeService();
            serviceGroupeEmp = new GroupesEmployeService();
        }
        // GET: Autorisation
        [HttpGet]
        public ActionResult Index()
        {
            String login = Session["loginIndex"].ToString();
            Employee item = serviceEmployees.getByLoginUser(login);
            var groupesassociees = serviceGroupeEmp.getGroupeByIDEmployee(item.Id);
            var usersAssociees=serviceGroupeEmp.getListEmployeeByGroupe(item.Id);
            EventViewModel a = new EventViewModel();
            var logins = serviceUser.GetAll();
            var tests = usersAssociees.Select(o => o.userLogin).Distinct().ToList();
            foreach (String test in tests)
            {
                a.utilisateurs.Add(new SelectListItem { Text = test, Value = test});
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
            return View(a);
        }
        [HttpPost]
        public ActionResult EnregistrerEvent(string NewPlanTitre, string NewPlanDescription, DateTime NewPlanDate, DateTime NewPlanDate2, string NewPlanTime, string NewPlanTime2, string NewplanGroups, string ThemeColor,string NewplanUser)
        {
            
            if (NewPlanTitre == "Autorisation") { ThemeColor = "#cc0000"; }
            else if (NewPlanTitre == "Congé") { ThemeColor = "#26A69A"; }
            else if (NewPlanTitre == "Jours Fériés") { ThemeColor = "#0099ff"; }
            else if (NewPlanTitre == "Fermeture Annuel") { ThemeColor = "#5C6BC0"; }
            else if (NewPlanTitre == "Planning") { ThemeColor = "#ff66ff"; }
            using (ReportContext context = new ReportContext())
            {
                Event eventt = new Event();
                if (NewplanUser != "")
                {
                    Employee emp = context.employees.FirstOrDefault(c => c.userLogin == NewplanUser);


                    eventt.titre = NewPlanTitre;
                    eventt.description = NewPlanDescription;
                    eventt.dateDebut = NewPlanDate;
                    eventt.dateFin = NewPlanDate2;
                    eventt.start = NewPlanTime;
                    eventt.end = NewPlanTime2;
                    eventt.themeColor = ThemeColor;
                    eventt.employeeId = emp.Id;



                    context.events.Add(eventt);
                    context.SaveChanges();

                }
                if (NewplanGroups != "")
                {
                    Groupe a = context.groupes.FirstOrDefault(c => c.nom == NewplanGroups);
                    eventt.titre = NewPlanTitre;
                    eventt.description = NewPlanDescription;
                    eventt.dateDebut = NewPlanDate;
                    eventt.dateFin = NewPlanDate2;
                    eventt.start = a.nom;//NewPlanTime;
                    eventt.end = NewPlanTime2;
                    eventt.themeColor = ThemeColor;
                    context.events.Add(eventt);
                    context.SaveChanges();
                    Event eventt2 = context.events.Find(eventt.Id);
                    eventt2.groupes.Add(a);
                    context.SaveChanges();
                    //it's imed i m using the context directly because i have two service and they call each a different instance of context and in this case we must have a single context to affect  two objects existing in the same context.
                }
                return RedirectToAction("Index");

            }
        }
        
        public ActionResult GetEvents()
        {
            using (ReportContext dc = new ReportContext())
            {
                var events = dc.events.ToList();
                foreach(var test in events)
                {
                    test.dateDebut=DateTime.Now;
                    test.dateFin= DateTime.Now;
                }
                ViewBag.eventsTest = events;
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        

    }
}
