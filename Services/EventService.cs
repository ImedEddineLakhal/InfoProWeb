using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using MyReports.Data.Infrastructure;
using MyFinance.Data.Infrastructure;


namespace Services
{
    public class EventService : IEventService
    {
        IDatabaseFactory dbfactory = null;
        IUnitOfWork uow = null;

        public EventService()
        {
            dbfactory = new DatabaseFactory();
            uow = new UnitOfWork(dbfactory);
        }
        public void Add(Event even)
        {
            uow.EventRepository.Add(even);
        }

        public void Delete(Event even)
        {
            uow.EventRepository.Delete(even);
        }

        public void Dispose()
        {
            uow.Dispose();

        }

        public Event findEventBy(string champ)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAll()
        {
            return uow.EventRepository.GetAll();
        }

        public Event getById(string champ)
        {
            throw new NotImplementedException();
        }

        public Event getById(int? id)
        {
            return uow.EventRepository.GetById(id);
        }

        public void SaveChange()
        {
            uow.Commit();
        }
    }
}
