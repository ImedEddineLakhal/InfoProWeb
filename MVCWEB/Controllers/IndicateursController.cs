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
using System.Globalization;

namespace MVCWEB.Controllers
{
  
    public class IndicateursController : Controller
    {
        IGroupeEmployeeService serviceGroupeEmp;
        IEmployeeService service;
        static int idEmpConnecte;

        private ReportContext db = new ReportContext();
        public IndicateursController()
        {           
            service = new EmployeeService();
            serviceGroupeEmp = new GroupesEmployeService();
            //SelectList Semaine
            var semaines = new List<SelectListItem>();         
            for (int m = 1; m <= 52; m++)
            {
                var val = m.ToString();
               
                semaines.Add(new SelectListItem { Text = "Semaine" + val, Value = val });
            }
            ViewBag.SemaineItems = semaines;
            // SelectList Trimestre
            var timestres = new List<SelectListItem>();
            timestres.Add(new SelectListItem { Text = "Trimestre1", Value = "1" });
            timestres.Add(new SelectListItem { Text = "Trimestre2", Value = "2" });
            timestres.Add(new SelectListItem { Text = "Trimestre3", Value = "3" });
            timestres.Add(new SelectListItem { Text = "Trimestre4", Value = "4" });
            ViewBag.TrimestreItems = timestres;
        }

    
        // GET: IndexParAgent Journalier View
        [HttpGet]
        public ActionResult Index( int ? id)
        {
            service = new EmployeeService();
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
           // groupesassocies.Insert(0, new SelectListItem { Text = "Sélectionner l'Agent", Value = "0" });
            foreach (var item in groupes)
            {
                List<Employee> emp = serviceGroupeEmp.getListEmployeeByGroupeId(item.Id);
                foreach (var e in emp)
                {
                    string IdHermes = e.IdHermes.ToString();
                    if (e.Id != empConnected.Id)
                    {
                        if (!(groupesassocies.Exists(x => x.Text == e.userName && x.Value == IdHermes)))
                        {
                            groupesassocies.Add(new SelectListItem { Text = e.userName, Value = IdHermes });
                        }
                    }
                }
            }
            ViewBag.AgentItems = groupesassocies;
            return View();
        }

        //Hebdo View Par Agent
        [HttpGet]
        public ActionResult Hebdo(int ? id)
        {
            service = new EmployeeService();
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
           // groupesassocies.Insert(0, new SelectListItem {Text = "Sélectionner l'Agent", Value = "0" });
            foreach (var item in groupes)
            {
                List<Employee> emp = serviceGroupeEmp.getListEmployeeByGroupeId(item.Id);
                foreach (var e in emp)
                {
                    string IdHermes = e.IdHermes.ToString();
                    if  (e.Id != empConnected.Id)
                        {
                     if (!(groupesassocies.Exists(x => x.Text == e.userName && x.Value == IdHermes))){
                            groupesassocies.Add(new SelectListItem { Text = e.userName, Value = IdHermes });
                        }
                    }
                }
            }
            ViewBag.AgentItems = groupesassocies;
          return View();
        }

        // Json data et calcul Journalier Par Agent
        public JsonResult GetJournalierValues(string agentSel, string daySel)
        {
            List<Calcul> calculs = new List<Calcul>();    
            var appels = db.appels.ToList();
            var temps = db.temps.ToList();
          
            double TotAccord = 0;
            double TotCA = 0;
            double TotCNA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;        
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            var dates = new List<DateTime>();
            foreach (var item in appels)
            {
                var d=new DateTime();                             
                if ((daySel).Equals(""))
                {
                    d = DateTime.Now;
                }
                else
                {
                    d = DateTime.Parse(daySel);
                }
                int ag = int.Parse(agentSel);
                if (item.date == d && item.Id_Hermes == ag)
                {
                    TotCA += item.CA;
                    TotCNA += item.CNA;
                    TotAccord += item.Accords;
                    TotAppelEmis += item.TotalAppelEmis;
                    TotAppelAboutis += item.TotalAppelAboutis;                   
                    if (!(dates.Exists(x => x == item.date)))
                    {
                        dates.Add(item.date);
                    }
                    TotJourTravaillés = dates.LongCount();
                }
               
            }

            foreach (var item in temps)
            {
                var d = new DateTime();
                if ((daySel).Equals(""))
                {
                    d = DateTime.Now;
                }
                else
                {
                    d = DateTime.Parse(daySel);
                }
                int ag = int.Parse(agentSel);

                if (item.date == d && item.Id_Hermes == ag)
                {
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;
                  
                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempscom + item.tempsAtt;

                    TotProdReel += (item.tempscom + item.tempsAtt )/ 360000;
                    tempsPresence += (item.tempsLog / 360000);

                }
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
                double ETPplanifie = Math.Round(((TotLog / 360000) / TotJourTravaillés / 8), 2);
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
                double TauxVentesHebdo = Math.Round(((TotAccord / TotCA) * 100), 2);
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
            }
            else
            {
                double TauxVentesHebdo = 0;
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = 0;

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            else
            {
                double TauxACWHebdo = 0;
                double TauxPreviewHebdo = 0;
                double TauxPauseBriefHebdo = 0;
                double TauxPausePersoHebdo = 0;
                double TauxOccupation = 0;
                double TauxComunication = 0;

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }

            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        //Json data et calcul spécifiques Hebdo Par Agent
        public JsonResult GetHebdoValues(string agentSel, string semaineSel)
        {
            List<Calcul> calculs = new List<Calcul>();
            var appels = db.appels.ToList();
            var temps = db.temps.ToList();          
            double TotAccord = 0;
            double TotCA = 0;
            double TotCNA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            var dates = new List<DateTime>();
            foreach (var item in appels)
            {
                var test = 0;
                if (semaineSel == null)
                {
                    test = 0;
                }
                else
                {
                    test = int.Parse(semaineSel);
                }
                //int test = int.Parse(semaineSel);
                int ag = int.Parse(agentSel);
                if (item.semaine == test && item.Id_Hermes == ag)
                {
                    TotCA += item.CA;
                    TotCNA += item.CNA;
                    TotAccord += item.Accords;
                    TotAppelEmis += item.TotalAppelEmis;
                    TotAppelAboutis += item.TotalAppelAboutis;
                    if (!(dates.Exists(x => x == item.date)))
                    {
                        dates.Add(item.date);
                    }
                    TotJourTravaillés = dates.LongCount();
                }
            }
            foreach (var item in temps)
            {
                var test = 0;
                if (semaineSel == null)
                {
                    test = 0;
                }
                else
                {
                    test = int.Parse(semaineSel);
                }
                int ag = int.Parse(agentSel);

                if (item.semaine == test && item.Id_Hermes == ag)
                {
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;

                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempscom + item.tempsAtt;

                    TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                    tempsPresence += (item.tempsLog / 360000);

                }
            }
            //tratement fiches
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
                double ETPplanifie = Math.Round(((TotLog / 360000) / TotJourTravaillés / 8), 2);
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
                double TauxVentesHebdo = Math.Round(((TotAccord / TotCA) * 100), 2);
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
            }
            else
            {
                double TauxVentesHebdo = 0;
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = 0;

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            else
            {
                double TauxACWHebdo = 0;
                double TauxPreviewHebdo = 0;
                double TauxPauseBriefHebdo = 0;
                double TauxPausePersoHebdo = 0;
                double TauxOccupation = 0;
                double TauxComunication = 0;

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        // View Mensuelle Par Agent
        [HttpGet]
        public ActionResult Mensuel(int ? id)
        {
            service = new EmployeeService();
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
            groupesassocies.Insert(0, new SelectListItem { Text = "Sélectionner l'Agent", Value = "0" });
            foreach (var item in groupes)
            {
                List<Employee> emp = serviceGroupeEmp.getListEmployeeByGroupeId(item.Id);
                foreach (var e in emp)
                {
                    string IdHermes = e.IdHermes.ToString();
                    if (e.Id != empConnected.Id)
                    {
                        if (!(groupesassocies.Exists(x => x.Text == e.userName && x.Value == IdHermes)))
                        {
                            groupesassocies.Add(new SelectListItem { Text = e.userName, Value = IdHermes });
                        }
                    }
                }
            }
            ViewBag.AgentItems = groupesassocies;
            return View();
        }

        //Json data et calcul spécifiques Mensuelle Par Agent
        public JsonResult GetMensuelValues(string moisSel, string agentSel)
        {
            List<Calcul> calculs = new List<Calcul>();
            var appels = db.appels.ToList();
            var temps = db.temps.ToList();
            double TotAccord = 0;
            double TotCA = 0;
            double TotCNA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            var dates = new List<DateTime>();
            foreach (var item in appels)
            {
                var test = 0;
               
                if (moisSel == null || moisSel.Equals(""))
                {
                    test = 0;
                }
                else
                {
                    test = int.Parse(moisSel);
                }
                int ag = int.Parse(agentSel);
                if (item.date.Month == test && item.Id_Hermes == ag)
                {
                    TotCA += item.CA;
                    TotCNA += item.CNA;
                    TotAccord += item.Accords;
                    TotAppelEmis += item.TotalAppelEmis;
                    TotAppelAboutis += item.TotalAppelAboutis;
                    if (!(dates.Exists(x => x == item.date)))
                    {
                        dates.Add(item.date);
                    }
                    TotJourTravaillés = dates.LongCount();
                }
            }
            foreach (var item in temps)
            {
                var test = 0;

                if (moisSel == null || moisSel.Equals(""))
                {
                    test = 0;
                }
                else
                {
                    test = int.Parse(moisSel);
                }
                int ag = int.Parse(agentSel);
                if (item.date.Month == test && item.Id_Hermes == ag)
                {
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;
                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempscom + item.tempsAtt;
                    TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                    tempsPresence += (item.tempsLog / 360000);
                }
            }

            //traitement
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
                double ETPplanifie = Math.Round(((TotLog / 360000) / TotJourTravaillés / 8), 2);
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
                double TauxVentesHebdo = Math.Round(((TotAccord / TotCA) * 100), 2);
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
            }
            else
            {
                double TauxVentesHebdo = 0;
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = 0;

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            else
            {
                double TauxACWHebdo = 0;
                double TauxPreviewHebdo = 0;
                double TauxPauseBriefHebdo = 0;
                double TauxPausePersoHebdo = 0;
                double TauxOccupation = 0;
                double TauxComunication = 0;

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        //View Annuelle
        [HttpGet]
        public ActionResult Annuelle(int ? id)
        {
            service = new EmployeeService();
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
            groupesassocies.Insert(0, new SelectListItem { Text = "Sélectionner l'Agent", Value = "0" });
            foreach (var item in groupes)
            {
                List<Employee> emp = serviceGroupeEmp.getListEmployeeByGroupeId(item.Id);
                foreach (var e in emp)
                {
                    string IdHermes = e.IdHermes.ToString();
                    if (e.Id != empConnected.Id)
                    {
                        if (!(groupesassocies.Exists(x => x.Text == e.userName && x.Value == IdHermes)))
                        {
                            groupesassocies.Add(new SelectListItem { Text = e.userName, Value = IdHermes });
                        }
                    }
                }
            }
            ViewBag.AgentItems = groupesassocies;
            return View();
        }



        //Json data et calcul spécifiques Annuelle Par Agent
        public JsonResult GetAnnuelValues(string anneeSel, string agentSel)
        {
            List<Calcul> calculs = new List<Calcul>();
            var appels = db.appels.ToList();
            var temps = db.temps.ToList();
            double TotAccord = 0;
            double TotCA = 0;
            double TotCNA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            var dates = new List<DateTime>();
            foreach (var item in appels)
            {
                var test = 0;

                if (anneeSel == null || anneeSel.Equals(""))
                {
                    test = 0;
                }
                else
                {
                    test = int.Parse(anneeSel);
                }
                
                int ag = int.Parse(agentSel);
                if (item.date.Year == test && item.Id_Hermes == ag)
                {
                    TotCA += item.CA;
                    TotCNA += item.CNA;
                    TotAccord += item.Accords;
                    TotAppelEmis += item.TotalAppelEmis;
                    TotAppelAboutis += item.TotalAppelAboutis;
                    if (!(dates.Exists(x => x == item.date)))
                    {
                        dates.Add(item.date);
                    }
                    TotJourTravaillés = dates.LongCount();
                }
            }
            foreach (var item in temps)
            {
                var test = 0;

                if (anneeSel == null || anneeSel.Equals(""))
                {
                    test = 0;
                }
                else
                {
                    test = int.Parse(anneeSel);
                }

                int ag = int.Parse(agentSel);
                if (item.date.Year == test && item.Id_Hermes == ag)
                {
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;

                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempscom + item.tempsAtt;

                    TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                    tempsPresence += (item.tempsLog / 360000);
                }
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
                double ETPplanifie = Math.Round(((TotLog / 360000) / TotJourTravaillés / 8), 2);
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
                double TauxVentesHebdo = Math.Round(((TotAccord / TotCA) * 100), 2);
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
            }
            else
            {
                double TauxVentesHebdo = 0;
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = 0;

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            else
            {
                double TauxACWHebdo = 0;
                double TauxPreviewHebdo = 0;
                double TauxPauseBriefHebdo = 0;
                double TauxPausePersoHebdo = 0;
                double TauxOccupation = 0;
                double TauxComunication = 0;

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        // View Trimestrielle
        [HttpGet]
        public ActionResult Trimestriel(int ? id)
        {
            service = new EmployeeService();
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
            groupesassocies.Insert(0, new SelectListItem { Text = "Sélectionner l'Agent", Value = "0" });
            foreach (var item in groupes)
            {
                List<Employee> emp = serviceGroupeEmp.getListEmployeeByGroupeId(item.Id);
                foreach (var e in emp)
                {
                    string IdHermes = e.IdHermes.ToString();
                    if (e.Id != empConnected.Id)
                    {
                        if (!(groupesassocies.Exists(x => x.Text == e.userName && x.Value == IdHermes)))
                        {
                            groupesassocies.Add(new SelectListItem { Text = e.userName, Value = IdHermes });
                        }
                    }
                }
            }
            ViewBag.AgentItems = groupesassocies;
            return View();
        }

        //Json data et calcul spécifiques Trimestriel Par Agent
        public JsonResult GetTrimestrielValues(string trimestreSel, string agentSel)
        {
            List<Calcul> calculs = new List<Calcul>();
            var appels = db.appels.ToList();
            var temps = db.temps.ToList();
            double TotAccord = 0;
            double TotCA = 0;
            double TotCNA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            var dates = new List<DateTime>();
        
            var test = 0;

            if (trimestreSel == null || trimestreSel.Equals(""))
            {
                test = 0;
            }
            else
            {
                test = int.Parse(trimestreSel);
            }
            int ag = int.Parse(agentSel);
            switch (test)
            {
                case 1:
                    foreach (var item in appels)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 1 || item.date.Month == 2 || item.date.Month == 3)
                            {
                                TotCA += item.CA;
                                TotCNA += item.CNA;
                                TotAccord += item.Accords;
                                TotAppelEmis += item.TotalAppelEmis;
                                TotAppelAboutis += item.TotalAppelAboutis;
                                if (!(dates.Exists(x => x == item.date)))
                                {
                                    dates.Add(item.date);
                                }
                                TotJourTravaillés = dates.LongCount();
                            }
                        }
                    }
                    foreach (var item in temps)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 1 || item.date.Month == 2 || item.date.Month == 3)
                            {
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;

                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempscom + item.tempsAtt;

                                TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                                tempsPresence += (item.tempsLog / 360000);
                            }
                        }
                    }

                    break;
                case 2:
                    foreach (var item in appels)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 4 || item.date.Month == 5 || item.date.Month == 6)
                            {
                                TotCA += item.CA;
                                TotCNA += item.CNA;
                                TotAccord += item.Accords;
                                TotAppelEmis += item.TotalAppelEmis;
                                TotAppelAboutis += item.TotalAppelAboutis;
                                if (!(dates.Exists(x => x == item.date)))
                                {
                                    dates.Add(item.date);
                                }
                                TotJourTravaillés = dates.LongCount();
                            }
                        }
                    }
                    foreach (var item in temps)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 4 || item.date.Month == 5 || item.date.Month == 6)
                            {
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;

                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempscom + item.tempsAtt;

                                TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                                tempsPresence += (item.tempsLog / 360000);
                            }
                        }
                    }
                    break;
                case 3:
                    foreach (var item in appels)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 7 || item.date.Month == 8|| item.date.Month == 9)
                            {
                                TotCA += item.CA;
                                TotCNA += item.CNA;
                                TotAccord += item.Accords;
                                TotAppelEmis += item.TotalAppelEmis;
                                TotAppelAboutis += item.TotalAppelAboutis;
                                if (!(dates.Exists(x => x == item.date)))
                                {
                                    dates.Add(item.date);
                                }
                                TotJourTravaillés = dates.LongCount();
                            }
                        }
                    }
                    foreach (var item in temps)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 7 || item.date.Month == 8 || item.date.Month == 9)
                            {
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;

                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempscom + item.tempsAtt;

                                TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                                tempsPresence += (item.tempsLog / 360000);
                            }
                        }
                    }
                    break;
                case 4:
                    foreach (var item in appels)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 10 || item.date.Month == 11 || item.date.Month == 12)
                            {
                                TotCA += item.CA;
                                TotCNA += item.CNA;
                                TotAccord += item.Accords;
                                TotAppelEmis += item.TotalAppelEmis;
                                TotAppelAboutis += item.TotalAppelAboutis;
                                if (!(dates.Exists(x => x == item.date)))
                                {
                                    dates.Add(item.date);
                                }
                                TotJourTravaillés = dates.LongCount();
                            }
                        }
                    }
                    foreach (var item in temps)
                    {
                        if (item.Id_Hermes == ag)
                        {
                            if (item.date.Month == 10 || item.date.Month == 11 || item.date.Month == 12)
                            {
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;

                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempscom + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempscom + item.tempsAtt;

                                TotProdReel += (item.tempscom + item.tempsAtt) / 360000;
                                tempsPresence += (item.tempsLog / 360000);
                            }
                        }
                    }
                    break;
            }

            //traitement
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
                double ETPplanifie = Math.Round(((TotLog / 360000) / TotJourTravaillés / 8), 2);
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
                double TauxVentesHebdo = Math.Round(((TotAccord / TotCA) * 100), 2);
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
            }
            else
            {
                double TauxVentesHebdo = 0;
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });
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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = 0;

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            else
            {
                double TauxACWHebdo = 0;
                double TauxPreviewHebdo = 0;
                double TauxPauseBriefHebdo = 0;
                double TauxPausePersoHebdo = 0;
                double TauxOccupation = 0;
                double TauxComunication = 0;

                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        //Controllers in Agent Template
        [HttpGet]
        public ActionResult JournalierAgent(int? id)
        {
            service = new EmployeeService();
            string value = (string)Session["loginIndex"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                idEmpConnecte = (int)id;
            }
            Employee empConnected = service.getById(id);
            if (empConnected.Content != null)
            {
                String strbase64 = Convert.ToBase64String(empConnected.Content);
                String empConnectedImage = "data:" + empConnected.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = empConnected.userName;
            ViewBag.pseudoNameEmpConnected = empConnected.pseudoName;
            return View(empConnected);
        }

        public ActionResult HebdoAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
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
            if (emp.Content != null)
            {
                String strbase64 = Convert.ToBase64String(emp.Content);
                String empConnectedImage = "data:" + emp.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = emp.userName;
            ViewBag.pseudoNameEmpConnected = emp.pseudoName;
            return View(emp);
        }

        public ActionResult MensuelAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
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
            if (emp.Content != null)
            {
                String strbase64 = Convert.ToBase64String(emp.Content);
                String empConnectedImage = "data:" + emp.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = emp.userName;
            ViewBag.pseudoNameEmpConnected = emp.pseudoName;
            return View(emp);
        }
        public ActionResult TrimestrielAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
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
            if (emp.Content != null)
            {
                String strbase64 = Convert.ToBase64String(emp.Content);
                String empConnectedImage = "data:" + emp.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = emp.userName;
            ViewBag.pseudoNameEmpConnected = emp.pseudoName;
            return View(emp);
        }
        public ActionResult AnnuelleAgent(int? id)
        {
            string value = (string)Session["loginIndex"];
            Employee empConnected = service.getById(idEmpConnecte);
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
            if (emp.Content != null)
            {
                String strbase64 = Convert.ToBase64String(emp.Content);
                String empConnectedImage = "data:" + emp.ContentType + ";base64," + strbase64;
                ViewBag.empConnectedImage = empConnectedImage;
            }
            ViewBag.nameEmpConnected = emp.userName;
            ViewBag.pseudoNameEmpConnected = emp.pseudoName;
            return View(emp);
        }

        public ActionResult TestStat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        //Fin Controllers in Agent Template



       
    }
}
