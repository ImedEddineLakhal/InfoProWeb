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
//    public class UtilsService : IUtilsService
//    {
//        IDatabaseFactory dbfactory = null;
//        IUnitOfWork uow = null;

//        public UtilsService()
//        {
//            dbfactory = new DatabaseFactory();
//            uow = new UnitOfWork(dbfactory);
//        }
//        public void Add(Utils utils)
//        {
//            uow.UtilsRepository.Add(utils);
//        }

//        public void Delete(Utils utils)
//        {
//            uow.UtilsRepository.Delete(utils);
//        }

//        public void Dispose()
//        {
//            uow.Dispose();

//        }

//        public Utils findUtilsBy(string champ)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Utils> GetAll()
//        {
//            return uow.UtilsRepository.GetAll();
//        }

//        public Utils getById(string champ)
//        {
//            throw new NotImplementedException();
//        }

//        public Utils getById(int? id)
//        {
//            return uow.UtilsRepository.GetById(id);
//        }

//        public void SaveChange()
//        {
//            uow.Commit();
//        }
//    }
//}
