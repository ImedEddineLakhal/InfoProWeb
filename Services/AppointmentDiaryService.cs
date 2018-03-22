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
    public class AppointmentDiaryService : IAppointmentDiaryService
    {
        IDatabaseFactory dbfactory = null;
        IUnitOfWork uow = null;

        public AppointmentDiaryService()
        {
            dbfactory = new DatabaseFactory();
            uow = new UnitOfWork(dbfactory);
        }
        public void Add(AppointmentDiary appointmentDiary)
        {
            uow.AppointmentDiaryRepository.Add(appointmentDiary);
        }

        public void Delete(AppointmentDiary appointmentDiary)
        {
            uow.AppointmentDiaryRepository.Delete(appointmentDiary);
        }

        public void Dispose()
        {
            uow.Dispose();

        }

        public AppointmentDiary findAppointmentDiaryBy(string champ)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppointmentDiary> GetAll()
        {
            return uow.AppointmentDiaryRepository.GetAll();
        }

        public AppointmentDiary getById(string champ)
        {
            throw new NotImplementedException();
        }

        public AppointmentDiary getById(int? id)
        {
            return uow.AppointmentDiaryRepository.GetById(id);
        }

        public void SaveChange()
        {
            uow.Commit();
        }
    }
}
