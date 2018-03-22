//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Entity;
//using MyReports.Data.Infrastructure;
//using MyFinance.Data.Infrastructure;

//namespace Services
//{
//    public class DiaryEventService : IDiaryEventService
//    {
//        IDatabaseFactory dbfactory = null;
//        IUnitOfWork uow = null;

//        public DiaryEventService()
//        {
//            dbfactory = new DatabaseFactory();
//            uow = new UnitOfWork(dbfactory);
//        }
//        public void Add(DiaryEvents diaryEvents)
//        {
//            uow.DiaryEventsRepository.Add(diaryEvents);
//        }

//        public void Delete(DiaryEvents diaryEvents)
//        {
//            uow.DiaryEventsRepository.Delete(diaryEvents);
//        }

//        public void Dispose()
//        {
//            uow.Dispose();

//        }

//        public DiaryEvents findDiaryEventsBy(string champ)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<DiaryEvents> GetAll()
//        {
//            return uow.DiaryEventsRepository.GetAll();
//        }

//        public DiaryEvents getById(string champ)
//        {
//            throw new NotImplementedException();
//        }

//        public DiaryEvents getById(int? id)
//        {
//            return uow.DiaryEventsRepository.GetById(id);
//        }

//        public void SaveChange()
//        {
//            uow.Commit();
//        }
//    }
//}
