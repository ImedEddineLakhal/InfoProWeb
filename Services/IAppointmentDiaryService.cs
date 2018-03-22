using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAppointmentDiaryService : IDisposable
    {
        void Add(AppointmentDiary appointmentDiary);

        void Delete(AppointmentDiary appointmentDiary);

        void SaveChange();
        AppointmentDiary findAppointmentDiaryBy(String champ);
        AppointmentDiary getById(int? id);
        AppointmentDiary getById(String champ);
        IEnumerable<AppointmentDiary> GetAll();

        void Dispose();
    }
}
