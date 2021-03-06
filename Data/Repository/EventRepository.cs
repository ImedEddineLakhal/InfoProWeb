﻿using Data.Repository;
using Domain.Entity;
using MyReports.Data.Infrastracture;
using MyReports.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
        ReportContext context = new ReportContext();

        public List<Event> getGroupesListByEvent(int id)
        {
            List<Event> eventsrep = new List<Event>();
            var events=context.events.Where(s => s.groupes.Any(c => c.Id == id));

            foreach(var test in events)
            {
                eventsrep.Add(test);
            }
           if(eventsrep != null)
            {
                return eventsrep;
            }
            return null;
        }
        public void AffectGroupeTOEvent(Groupe groupe,Event eventt)
        {
            Event event2 = context.events.Find(eventt.Id);

            event2.groupes.Add(groupe);

            //call SaveChanges from context to confirm inserts
            context.SaveChanges();

        }
        public List<Event> getListEventsByEmployeeId(int id)
        {
            var events = context.events.Where(s => s.employeeId==id);
            List<Event> tests = new List<Event>();

            foreach (var test in events)
            {
                tests.Add(test);
            }
            return tests;
        }
        public List<Event> getListEventsByListGroupes(List<Groupe> groupes)
        {
            List<Event> eventsrep = new List<Event>();
            foreach (var groupe in groupes) { 
            var events = context.events.Where(s => s.groupes.Any(c => c.Id == groupe.Id));
                
                eventsrep.AddRange(events);

            }     

            if (eventsrep != null)
            {
                return eventsrep;
            }
            return null;
        } 
    }
}