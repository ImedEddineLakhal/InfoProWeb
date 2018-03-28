using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEventService : IDisposable
    {
        void Add(Event even);

        void Delete(Event even);

        void SaveChange();
        Event findEventBy(String champ);
        Event getById(int? id);
        Event getById(String champ);
        IEnumerable<Event> GetAll();

        void Dispose();
    }
}
