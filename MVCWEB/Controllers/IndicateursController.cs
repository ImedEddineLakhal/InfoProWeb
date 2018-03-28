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
        IIndicateurService service;

        private ReportContext db = new ReportContext();

        public IndicateursController()
        {
            service = new IndicateurService();

            var titres = db.titres.ToList();

            var employees = db.employees.ToList();
            var emp = new List<SelectListItem>();
            foreach (var test in employees)
            {
                string IdHermes = test.IdHermes.ToString();
                emp.Add(new SelectListItem { Text = test.userName, Value = IdHermes });
            }
            ViewBag.AgentItems = emp;
            //SelectList Semaine
            var semaines = new List<SelectListItem>();
            semaines.Add(new SelectListItem { Text = "Semaine1", Value = "1" });
            semaines.Add(new SelectListItem { Text = "Semaine2", Value = "2" });
            semaines.Add(new SelectListItem { Text = "Semaine3", Value = "3" });
            semaines.Add(new SelectListItem { Text = "Semaine4", Value = "4" });
            semaines.Add(new SelectListItem { Text = "Semaine5", Value = "5" });
            semaines.Add(new SelectListItem { Text = "Semaine6", Value = "6" });
            semaines.Add(new SelectListItem { Text = "Semaine7", Value = "7" });
            semaines.Add(new SelectListItem { Text = "Semaine8", Value = "8" });
            semaines.Add(new SelectListItem { Text = "Semaine9", Value = "9" });
            semaines.Add(new SelectListItem { Text = "Semaine10", Value = "10" });
            semaines.Add(new SelectListItem { Text = "Semaine11", Value = "11" });
            semaines.Add(new SelectListItem { Text = "Semaine12", Value = "12" });
            semaines.Add(new SelectListItem { Text = "Semaine13", Value = "13" });
            semaines.Add(new SelectListItem { Text = "Semaine14", Value = "14" });
            semaines.Add(new SelectListItem { Text = "Semaine15", Value = "15" });
            semaines.Add(new SelectListItem { Text = "Semaine16", Value = "16" });
            semaines.Add(new SelectListItem { Text = "Semaine17", Value = "17" });
            semaines.Add(new SelectListItem { Text = "Semaine18", Value = "18" });
            semaines.Add(new SelectListItem { Text = "Semaine19", Value = "19" });
            semaines.Add(new SelectListItem { Text = "Semaine20", Value = "20" });
            ViewBag.SemaineItems = semaines;
            // SelectList Trimestre
            var timestres = new List<SelectListItem>();
            timestres.Add(new SelectListItem { Text = "Trimestre1", Value = "1" });
            timestres.Add(new SelectListItem { Text = "Trimestre2", Value = "2" });
            timestres.Add(new SelectListItem { Text = "Trimestre3", Value = "3" });
            timestres.Add(new SelectListItem { Text = "Trimestre4", Value = "4" });
            ViewBag.TrimestreItems = timestres;

        }


        // GET: TestIndicateurs/IndexParAgent
        public ActionResult Index()
        {
            // var indicateurs = service.GetAll();
            return View();
        }

        // GET: TestIndicateurs/IndexParTitre
        public ActionResult IndexParTitre()
        {

            return View(db.titres.ToList());
        }

        public ActionResult DetailsTitre(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titre titre = db.titres.Find(id);
            if (titre == null)
            {
                return HttpNotFound();
            }
            return View(titre);
        }

        public JsonResult CalculTitreHebdo(int idtitre, int semaineSel, string libelletitre, string activitetitre, string typetitre)
        {
            List<Calcul> calculs = new List<Calcul>();
            var indicateurs = db.indicateurs.ToList();
            double TotCA = 0;
            double TotAccord = 0;
            double TotCNA = 0;
            double tempsCommunicationGlobal = 0;
            foreach (var item in indicateurs)
            {
                if (item.semaine == semaineSel)
                {
                    TotCA += item.CA;
                    TotAccord += item.accord;
                    TotCNA += item.CNA;
                    tempsCommunicationGlobal += item.tempsComm;
                }
            }
            if (TotCA != 0)
            {
                double TauxAccord = Math.Round((TotAccord / TotCA), 2) * 100;
                calculs.Add(new Calcul { value = TauxAccord, name = "Taux CA+" });
            }
            else
            {
                double TauxAccord = 0;
                calculs.Add(new Calcul { value = TauxAccord, name = "Taux CA+" });
            }

            calculs.Add(new Calcul { value = TotCA, name = "Nombre CA" });
            calculs.Add(new Calcul { value = TotAccord, name = "Nombre CA+" });
            calculs.Add(new Calcul { value = TotCNA, name = "Nombre CNA" });



            calculs.Add(new Calcul { value = tempsCommunicationGlobal, name = "Durée de communication global" });
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }


        //Hebdo View Par Agent
        public ActionResult Hebdo()
        {
            return View();
        }

        // Json data et calcul Journalier Par Agent
        public JsonResult GetJournalierValues(string agentSel, string daySel)
        {
            List<Calcul> calculs = new List<Calcul>();
            var indicateurs = db.indicateurs.ToList();
            //var indicateurs = service.findIndicateurBy(agentSel);
            double TotAccord = 0;
            double TotCA = 0;
            double TotCNA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPauseBrief = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotAppelAboutis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            foreach (var item in indicateurs)
            {
                var d=new DateTime();
                //try
                //{
                //     d = DateTime.Parse(daySel);
                //}
                //catch (DataException )  {
                //    Console.WriteLine("Erreur  aaaa");
                //}
                //finally
                //{
                //    d = new DateTime();

                //}
               
                if ((daySel).Equals(""))
                {
                    d = DateTime.Now;
                }
                else
                {
                    d = DateTime.Parse(daySel);
                }
                int ag = int.Parse(agentSel);
                if (item.date == d && item.agent == ag)
                {
                    TotCA += item.CA;
                    TotCNA += item.CNA;
                    TotAccord += item.accord;
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;
                    TotPauseBrief += item.tempsPauseBrief;
                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempsComm + item.tempsAtt;
                    TotAppelEmis += item.appelEmis;
                    TotAppelAboutis += item.totAboutis;
                    TotProdReel += Math.Round((item.tempsComm / 360000) + (item.tempsAtt / 360000), 2);
                    tempsPresence += Math.Round((item.tempsLog / 360000), 2);
                    TotJourTravaillés += 1;
                }
            }
            //téléphonie
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

            //taux traitement fiches
            if (TotLog != 0)
            {
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = Math.Round(((TotPauseBrief / TotLog) * 100), 2);

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
        public JsonResult GetHebdoValues(string semaineSel, string agentSel)
        {
            List<Calcul> calculs = new List<Calcul>();
            var indicateurs = db.indicateurs.ToList();
            //var indicateurs = service.findIndicateurBy(agentSel);

            //var d = (semaineSel);
            //CultureInfo cul = CultureInfo.CurrentCulture;

            //var firstDayWeek = cul.Calendar.GetWeekOfYear(
            //    d,
            //    CalendarWeekRule.FirstDay,
            //    DayOfWeek.Monday);

            //int weekNum = cul.Calendar.GetWeekOfYear(
            //    d,
            //    CalendarWeekRule.FirstDay,
            //    DayOfWeek.Monday);

            double TotAccord = 0;
            double TotCA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPauseBrief = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            double TotJourTravaillés = 0;
            foreach (var item in indicateurs)
            {
                int test = int.Parse(semaineSel);
                int ag = int.Parse(agentSel);
                if (item.semaine == test && item.agent == ag)
                {
                    TotCA += item.CA;
                    TotAccord += item.accord;
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;
                    TotPauseBrief += item.tempsPauseBrief;
                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                    TotCommunication += Math.Round((item.tempsComm + item.tempsAtt), 3);
                    TotAppelEmis += item.appelEmis;
                    TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                    tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                    TotJourTravaillés += 1;
                }
            }

            calculs.Add(new Calcul { value = TotAppelEmis, name = "Total Appel Emis" });
            calculs.Add(new Calcul { value = TotCA, name = "Nombre CA" });
            calculs.Add(new Calcul { value = TotAccord, name = "Nombre CA+ global" });
            calculs.Add(new Calcul { value = TotJourTravaillés, name = "Nombre des jours travailés" });
            calculs.Add(new Calcul { value = tempsPresence, name = "Temps de Présence/Heure" });
            calculs.Add(new Calcul { value = TotProdReel, name = "Temps de Prod réel/Heure" });

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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = Math.Round(((TotPauseBrief / TotLog) * 100), 2);

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                double TauxVenteParHeure = Math.Round(((TotAccord / (TotLog / 3600)) * 100), 2);

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
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
                double TauxVenteParHeure = 0;

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            if (TotJourTravaillés != 0)
            {
                double ETPplanifie = Math.Round(((TotLog / 3600) / TotJourTravaillés / 8), 2);

                double MoyenneAccord = Math.Round((TotAccord / TotJourTravaillés / ETPplanifie), 2);

                double MoyenneAppels = Math.Round((TotAppelEmis / TotJourTravaillés / ETPplanifie), 2);

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });
            }
            else
            {
                double ETPplanifie = 0;
                double MoyenneAccord = 0;
                double MoyenneAppels = 0;

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });

            }
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        // View Mensuelle Par Agent
        public ActionResult Mensuel()
        {
            return View();
        }

        //Json data et calcul spécifiques Mensuelle Par Agent
        public JsonResult GetMensuelValues(string moisSel, string agentSel)
        {

            List<Calcul> calculs = new List<Calcul>();
            var indicateurs = db.indicateurs.ToList();
            double TotAccord = 0;
            double TotCA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPauseBrief = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            int TotJourTravaillés = 0;
            foreach (var item in indicateurs)
            {
                int test = int.Parse(moisSel);
                int ag = int.Parse(agentSel);
                if (item.date.Month == test && item.agent == ag)
                {
                    TotCA += item.CA;
                    TotAccord += item.accord;
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;
                    TotPauseBrief += item.tempsPauseBrief;
                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempsComm + item.tempsAtt;
                    TotAppelEmis += item.appelEmis;
                    TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                    tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                    TotJourTravaillés += 1;
                }
            }

            calculs.Add(new Calcul { value = TotAppelEmis, name = "Total Appel Emis" });
            calculs.Add(new Calcul { value = TotCA, name = "Nombre CA" });
            calculs.Add(new Calcul { value = TotAccord, name = "Nombre CA+ global" });
            calculs.Add(new Calcul { value = TotJourTravaillés, name = "Nombre des jours travailés" });
            calculs.Add(new Calcul { value = tempsPresence, name = "Temps de Présence/Heure" });
            calculs.Add(new Calcul { value = TotProdReel, name = "Temps de Prod réel/Heure" });

            double TauxVentesHebdo = Math.Round(((TotAccord / TotCA) * 100), 2);
            if (double.IsNaN(TauxVentesHebdo))
            {
                calculs.Add(new Calcul { value = 0, name = "Taux de ventes(CA+)" });

            }
            else
            {
                calculs.Add(new Calcul { value = TauxVentesHebdo, name = "Taux de ventes(CA+)" });

            }

            if (TotLog != 0)
            {
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = Math.Round(((TotPauseBrief / TotLog) * 100), 2);

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                double TauxVenteParHeure = Math.Round(((TotAccord / (TotLog / 3600)) * 100), 2);

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
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
                double TauxVenteParHeure = 0;

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            if (TotJourTravaillés != 0)
            {
                double ETPplanifie = Math.Round(((TotLog / 3600) / TotJourTravaillés / 8), 2);

                double MoyenneAccord = Math.Round((TotAccord / TotJourTravaillés / ETPplanifie), 2);

                double MoyenneAppels = Math.Round((TotAppelEmis / TotJourTravaillés / ETPplanifie), 2);

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });
            }
            else
            {
                double ETPplanifie = 0;
                double MoyenneAccord = 0;
                double MoyenneAppels = 0;

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });

            }
            return Json(calculs, JsonRequestBehavior.AllowGet);
        }

        //View Annuelle
        public ActionResult Annuelle()
        {
            return View();
        }



        //Json data et calcul spécifiques Annuelle Par Agent
        public JsonResult GetAnnuelValues(string anneeSel, string agentSel)
        {

            List<Calcul> calculs = new List<Calcul>();
            var indicateurs = db.indicateurs.ToList();
            double TotAccord = 0;
            double TotCA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPauseBrief = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            int TotJourTravaillés = 0;
            foreach (var item in indicateurs)
            {
                int test = int.Parse(anneeSel);
                int ag = int.Parse(agentSel);
                if (item.date.Year == test && item.agent == ag)
                {
                    TotCA += item.CA;
                    TotAccord += item.accord;
                    TotAcw += item.tempsACW;
                    TotLog += item.tempsLog;
                    TotPreview += item.tempsPreview;
                    TotPauseBrief += item.tempsPauseBrief;
                    TotPausePerso += item.tempsPausePerso;
                    TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                    TotCommunication += item.tempsComm + item.tempsAtt;
                    TotAppelEmis += item.appelEmis;
                    TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                    tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                    TotJourTravaillés += 1;
                }
            }
            calculs.Add(new Calcul { value = TotAppelEmis, name = "Total Appel Emis" });
            calculs.Add(new Calcul { value = TotCA, name = "Nombre CA" });
            calculs.Add(new Calcul { value = TotAccord, name = "Nombre CA+ global" });
            calculs.Add(new Calcul { value = TotJourTravaillés, name = "Nombre des jours travailés" });
            calculs.Add(new Calcul { value = tempsPresence, name = "Temps de Présence/Heure" });
            calculs.Add(new Calcul { value = TotProdReel, name = "Temps de Prod réel/Heure" });

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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = Math.Round(((TotPauseBrief / TotLog) * 100), 2);

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                double TauxVenteParHeure = Math.Round(((TotAccord / (TotLog / 3600)) * 100), 2);

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
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
                double TauxVenteParHeure = 0;

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            if (TotJourTravaillés != 0)
            {
                double ETPplanifie = Math.Round(((TotLog / 3600) / TotJourTravaillés / 8), 2);

                double MoyenneAccord = Math.Round((TotAccord / TotJourTravaillés / ETPplanifie), 2);

                double MoyenneAppels = Math.Round((TotAppelEmis / TotJourTravaillés / ETPplanifie), 2);

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });
            }
            else
            {
                double ETPplanifie = 0;
                double MoyenneAccord = 0;
                double MoyenneAppels = 0;

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });

            }


            return Json(calculs, JsonRequestBehavior.AllowGet);
        }



        // View Trimestrielle
        public ActionResult Trimestriel()
        {
            return View();
        }

        //Json data et calcul spécifiques Trimestriel Par Agent
        public JsonResult GetTrimestrielValues(string trimestreSel, string agentSel)
        {

            List<Calcul> calculs = new List<Calcul>();
            var indicateurs = db.indicateurs.ToList();
            double TotAccord = 0;
            double TotCA = 0;
            double TotAcw = 0;
            double TotLog = 0;
            double TotPreview = 0;
            double TotPauseBrief = 0;
            double TotPausePerso = 0;
            double TotOccupation = 0;
            double TotCommunication = 0;
            double TotAppelEmis = 0;
            double TotProdReel = 0;
            double tempsPresence = 0;
            int TotJourTravaillés = 0;
            int test = int.Parse(trimestreSel);
            int ag = int.Parse(agentSel);
            switch (test)
            {
                case 1:
                    foreach (var item in indicateurs)
                    {
                        if (item.agent == ag)
                        {
                            if (item.date.Month == 1 || item.date.Month == 2 || item.date.Month == 3)
                            {
                                TotCA += item.CA;
                                TotAccord += item.accord;
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;
                                TotPauseBrief += item.tempsPauseBrief;
                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempsComm + item.tempsAtt;
                                TotAppelEmis += item.appelEmis;
                                TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                                tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                                TotJourTravaillés += 1;
                            }
                        }
                    }

                    break;
                case 2:
                    foreach (var item in indicateurs)
                    {
                        if (item.agent == ag)
                        {
                            if (item.date.Month == 4 || item.date.Month == 5 || item.date.Month == 6)
                            {
                                TotCA += item.CA;
                                TotAccord += item.accord;
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;
                                TotPauseBrief += item.tempsPauseBrief;
                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempsComm + item.tempsAtt;
                                TotAppelEmis += item.appelEmis;
                                TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                                tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                                TotJourTravaillés += 1;
                            }
                        }
                    }
                    break;
                case 3:
                    foreach (var item in indicateurs)
                    {
                        if (item.agent == ag)
                        {
                            if (item.date.Month == 7 || item.date.Month == 8 || item.date.Month == 9)
                            {
                                TotCA += item.CA;
                                TotAccord += item.accord;
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;
                                TotPauseBrief += item.tempsPauseBrief;
                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempsComm + item.tempsAtt;
                                TotAppelEmis += item.appelEmis;
                                TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                                tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                                TotJourTravaillés += 1;
                            }
                        }
                    }
                    break;
                case 4:
                    foreach (var item in indicateurs)
                    {
                        if (item.agent == ag)
                        {
                            if (item.date.Month == 10 || item.date.Month == 11 || item.date.Month == 12)
                            {
                                TotCA += item.CA;
                                TotAccord += item.accord;
                                TotAcw += item.tempsACW;
                                TotLog += item.tempsLog;
                                TotPreview += item.tempsPreview;
                                TotPauseBrief += item.tempsPauseBrief;
                                TotPausePerso += item.tempsPausePerso;
                                TotOccupation += item.tempsComm + item.tempsAtt + item.tempsPreview;
                                TotCommunication += item.tempsComm + item.tempsAtt;
                                TotAppelEmis += item.appelEmis;
                                TotProdReel += Math.Round((item.tempsComm / 3600) + (item.tempsAtt / 3600), 2);
                                tempsPresence += Math.Round((item.tempsLog / 3600), 2);
                                TotJourTravaillés += 1;
                            }
                        }
                    }
                    break;
            }
            calculs.Add(new Calcul { value = TotAppelEmis, name = "Total Appel Emis" });
            calculs.Add(new Calcul { value = TotCA, name = "Nombre CA" });
            calculs.Add(new Calcul { value = TotAccord, name = "Nombre CA+ global" });
            calculs.Add(new Calcul { value = TotJourTravaillés, name = "Nombre des jours travailés" });
            calculs.Add(new Calcul { value = tempsPresence, name = "Temps de Présence/Heure" });
            calculs.Add(new Calcul { value = TotProdReel, name = "Temps de Prod réel/Heure" });

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
                double TauxACWHebdo = Math.Round(((TotAcw / TotLog) * 100), 2);

                double TauxPreviewHebdo = Math.Round(((TotPreview / TotLog) * 100), 2);

                double TauxPauseBriefHebdo = Math.Round(((TotPauseBrief / TotLog) * 100), 2);

                double TauxPausePersoHebdo = Math.Round(((TotPausePerso / TotLog) * 100), 2);

                double TauxOccupation = Math.Round(((TotOccupation / TotLog) * 100), 2);

                double TauxComunication = Math.Round(((TotCommunication / TotLog) * 100), 2);

                double TauxVenteParHeure = Math.Round(((TotAccord / (TotLog / 3600)) * 100), 2);

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
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
                double TauxVenteParHeure = 0;

                calculs.Add(new Calcul { value = TauxVenteParHeure, name = "Taux de Ventes/Heure" });
                calculs.Add(new Calcul { value = TauxComunication, name = "Taux de Communication" });
                calculs.Add(new Calcul { value = TauxOccupation, name = "Taux d'occupation" });
                calculs.Add(new Calcul { value = TauxACWHebdo, name = "Taux ACW(Post-Appel)" });
                calculs.Add(new Calcul { value = TauxPreviewHebdo, name = "Taux Preview" });
                calculs.Add(new Calcul { value = TauxPauseBriefHebdo, name = "Taux Pause Brief" });
                calculs.Add(new Calcul { value = TauxPausePersoHebdo, name = "Taux Pause Perso" });
            }
            if (TotJourTravaillés != 0)
            {
                double ETPplanifie = Math.Round(((TotLog / 3600) / TotJourTravaillés / 8), 2);

                double MoyenneAccord = Math.Round((TotAccord / TotJourTravaillés / ETPplanifie), 2);

                double MoyenneAppels = Math.Round((TotAppelEmis / TotJourTravaillés / ETPplanifie), 2);

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });
            }
            else
            {
                double ETPplanifie = 0;
                double MoyenneAccord = 0;
                double MoyenneAppels = 0;

                calculs.Add(new Calcul { value = ETPplanifie, name = "ETP planifié" });
                calculs.Add(new Calcul { value = MoyenneAccord, name = "Moyenne CA+" });
                calculs.Add(new Calcul { value = MoyenneAppels, name = "Moyenne des Appels" });

            }


            return Json(calculs, JsonRequestBehavior.AllowGet);
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
