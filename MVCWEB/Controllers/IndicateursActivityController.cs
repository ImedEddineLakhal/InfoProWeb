using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWEB;
using MVCWEB.Models;
using Domain.Entity;
using Services;
using Data;

namespace MVCWEB.Controllers
{
    public class IndicateursActivityController : Controller
    {
        IGroupeEmployeeService serviceGroupeEmp;
        IEmployeeService service;
        IAttendanceHermesService serviceAttHermes;
        static int idEmpConnecte;
        private ReportContext db = new ReportContext();

        public IndicateursActivityController()
        {
            service = new EmployeeService();
            serviceGroupeEmp = new GroupesEmployeService();
            serviceAttHermes = new AttendanceHermesService();
        }


        // GET: IndicateursActivity
        [HttpGet]
        public ActionResult JournalierActivity(int? id)
        {     
            string value = (string)Session["loginIndex"];
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            idEmpConnecte = (int)id;
            Employee empConnected = service.getById(idEmpConnecte);
            if (empConnected.Content != null)
            {
                String strbase64 = Convert.ToBase64String(empConnected.Content);
                String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = empConnected.userName;
            ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;

            List<Groupe> groupes = serviceGroupeEmp.getGroupeByIDEmployee(id);
            List<SelectListItem> groupesassocies = new List<SelectListItem>();
            foreach (var item in groupes)
            {
                var Idgroupe = item.Id.ToString();
           groupesassocies.Add(new SelectListItem { Text = item.nom, Value = Idgroupe });
                 
            }
            ViewBag.GroupeItems = groupesassocies;
            return View();
        }

        public JsonResult GetJournalierValues(int groupeSel, string typeSel, DateTime daySel)
        {
            List<Calcul> calculs = new List<Calcul>();
            List<Employee> emp = serviceGroupeEmp.getListEmployeeByGroupeId(groupeSel);
            List<Employee> empassocies = new List<Employee>();
            foreach (var e in emp)
            {
            if(e.Id != idEmpConnecte)
                {
                    empassocies.Add(e);
                }
                    }                           
               
           
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotCA = 0;
            double TotAccord = 0;        
            double TotCNA = 0;
            double TotJourTravaillés = 0;
            var indicateurs = new List<Indicateur>();
            var appels = new List<Appel>();
            var temps = new List<Temps>();
          var attendances = new List<AttendanceHermes>();
            var dates = new List<DateTime>();
            foreach (var e in empassocies)
            {
                var app = db.appels.Where(a => a.Id_Hermes == e.IdHermes && a.date == daySel).ToList();
                appels.AddRange(app);
                var te = db.temps.Where(t => t.Id_Hermes == e.IdHermes && t.date == daySel).ToList();
                temps.AddRange(te);

                var att = db.attendancesHermes.Where(at => at.Id_Hermes == e.IdHermes && at.date == daySel).ToList();
                attendances.AddRange(att);
            }
                   
                foreach (var item in appels)
                {            
                    //if (item.nomCompagne == "GMT_REAB")
                    //{
                     TotAppelEmis += item.TotalAppelEmis;
                     TotAppelAboutis += item.TotalAppelAboutis;
                        TotCA += item.CA;
                        TotAccord += item.Accords;
                        TotCNA += item.CNA;
                if (!(dates.Exists(x => x == item.date)))
                {
                    dates.Add(item.date);
                }
                TotJourTravaillés = dates.LongCount();
                // }
            }
            double dep = 0;
            double arr = 0;
            double TotLog = 0;          
            foreach (var item in attendances)
            {
                 dep += (item.Depart.Value).Hour;
                 arr += (item.Arrive.Value).Hour;
                TotLog = (dep - arr)*360000;            
            }
            
            double TotCommunication = 0;
            double TotOccupation = 0;
            double TotAcw = 0;
            double TotPreview = 0;
            double TotPausePerso = 0;         
            double tempsPresence = 0;
            double TotProdReel = 0;
            foreach (var item in temps)
            {
                       // TotLog = item.tempsLog;
                        TotCommunication += item.tempscom + item.tempsAtt;
                        TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                        TotAcw += item.tempsACW;
                        TotPreview += item.tempsPreview;
                        TotPausePerso += item.tempsPausePerso;
                       // tempsPresence += (item.tempsLog / 360000);
                        TotProdReel += (item.tempscom / 360000) + (item.tempsAtt / 360000);
                 
            }
           
            //traitement fiches
            calculs.Add(new Calcul { value = TotAppelEmis, name = "Appels Emis" });
            calculs.Add(new Calcul { value = TotAppelAboutis, name = "Appels Aboutis" });
            calculs.Add(new Calcul { value = TotCA, name = "Contact Argumenté" });
            calculs.Add(new Calcul { value = TotAccord, name = "Contact Argumenté Positif" });
            calculs.Add(new Calcul { value = TotCNA, name = "Contact Argumenté Négatif" });
            if (TotJourTravaillés == 0)
            {
                double MoyenneAccord = 0;
                double MoyenneAppels = 0;

                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });

            }
            else
            {
                double ETPplanifie = Math.Round(((TotLog /360000) / TotJourTravaillés / 8), 2);
                if (ETPplanifie != 0)
                {
                    double MoyenneAccord = Math.Round((TotAccord / TotJourTravaillés / ETPplanifie), 2);

                    double MoyenneAppels = Math.Round((TotAppelEmis / TotJourTravaillés / ETPplanifie), 2);

                    calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                    calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });
                }
                else
                {
                    double MoyenneAccord = 0;
                    double MoyenneAppels = 0;

                    calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                    calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });
                }
            }
            if (TotCA != 0)
            {
                double TauxVentes = Math.Round(((TotAccord / TotCA) * 100), 2);
                calculs.Add(new Calcul { value = TauxVentes, name = "Taux de ventes(CA+)" });
            }
            else
            {
                double TauxVentes = 0;
                calculs.Add(new Calcul { value = TauxVentes, name = "Taux de ventes(CA+)" });
            }
            if (TotLog != 0)
            {
                double TauxVenteParHeure = Math.Round((TotAccord / (TotLog / 360000)), 2);
                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
            }
            else
            {
                double TauxVenteParHeure = 0;
                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
            }

            //temps présence
            tempsPresence = TotLog / 360000;
            calculs.Add(new Calcul { value = TotJourTravaillés, name = "Nombre des jours travailés" });
            calculs.Add(new Calcul { value = tempsPresence, name = "Temps de Présence/Heure" });
            calculs.Add(new Calcul { value = TotProdReel, name = "Temps de Prod réel/Heure" });
            if (TotJourTravaillés != 0)
            {
                double ETPplanifie = Math.Round(((TotLog / 360000) / TotJourTravaillés / 8), 2);
                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
            }
            else
            {
                double ETPplanifie = 0;
                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
            }
            //téléphonie
            if (TotLog != 0)
            {
                double TauxACW = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreview = Math.Round(((TotPreview / TotLog) * 100), 2);              

                double TauxPausePerso = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACW, name = "Taux ACW" });
                calculs.Add(new Calcul { value = TauxPreview, name = "Taux Preview" });               
                calculs.Add(new Calcul { value = TauxPausePerso, name = "Taux Pause Perso" });
            }
            else
            {
                double TauxACW = 0;
                double TauxPreview = 0;
                double TauxPausePerso = 0;
                double TauxOccupation = 0;
                double TauxComunication = 0;

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACW, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreview, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPausePerso, name = "Taux Pause Perso" });
            }

            return Json(calculs, JsonRequestBehavior.AllowGet);
        }


    }
}
