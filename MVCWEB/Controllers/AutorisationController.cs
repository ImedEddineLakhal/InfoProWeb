using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Entity;
using Services;


namespace MVCWEB.Controllers
{
    public class AutorisationController : Controller
    {
        IEventService service;
        public AutorisationController()
        {
            service = new EventService();
        }
        // GET: Autorisation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EnregistrerEvent(string NewPlanTitre,string NewPlanDescription, DateTime NewPlanDate,DateTime NewPlanDate2, string NewPlanTime, string NewPlanTime2,string ThemeColor)
        {
            if (NewPlanTitre=="Autorisation") { ThemeColor = "Red"; }
            if (NewPlanTitre == "Congé") { ThemeColor = "Blue"; }
            Event eventt = new Event
            {
                
                titre = NewPlanTitre,
                description = NewPlanDescription,
                dateDebut = NewPlanDate,
                dateFin = NewPlanDate2,
                start = NewPlanTime,
                end = NewPlanTime2,
                themeColor = ThemeColor
                
            };
            service.Add(eventt);
            service.SaveChange();
            return View("~/Views/Autorisation/Index.cshtml");
        }

        public JsonResult GetEvents()
        {
            using (ReportContext dc = new ReportContext())
            {
                var events = dc.events.ToList();
                foreach(var test in events)
                {
                    test.dateDebut=DateTime.Now;
                    test.dateFin= DateTime.Now;
                }
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        

    }
}
