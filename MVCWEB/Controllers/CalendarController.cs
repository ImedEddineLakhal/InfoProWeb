using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWEB.Models;
using Domain.Entity;
using System.Globalization;
using Services;


namespace MVCWEB.Models
{
    public class CalendarController : Controller
    {
        IAppointmentDiaryService service;
        public CalendarController()
        {

            service = new AppointmentDiaryService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDiaryEvents(String search, FormCollection form)
        {
            var users = service.GetAll();
            List<AppointmentDiary> fVM = new List<AppointmentDiary>();


            foreach (var item in users)
            {
                fVM.Add(item);
            }
            if (!String.IsNullOrEmpty(search))
            {

                fVM = fVM.Where(p => p.Title.ToLower().Contains(search.ToLower())).ToList<AppointmentDiary>();
           
        }
            return Json(fVM, JsonRequestBehavior.AllowGet);
        }
        




    }
}
