using Domain.Entity;
using MyReports.Data.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IEventRepository:IRepositoryBase<Event>
    {
        List<Event> getGroupesListByEvent(int id);
        void AffectGroupeTOEvent(Groupe groupe, Event eventt);
        List<Event> getListEventsByEmployeeId(int id);
        List<Event> getListEventsByListGroupes(List<Groupe> groupes);
    }
}
