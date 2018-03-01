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
    public class UserService : IUserService
    {
        IDatabaseFactory dbfactory = null;
        IUnitOfWork uow = null;

        public UserService()
        {
            dbfactory = new DatabaseFactory();
            uow = new UnitOfWork(dbfactory);
        }
        public void Add(User user)
        {

            uow.UserRepository.Add(user); 
                }

        public void Delete(User user)
        {
            uow.UserRepository.Delete(user);
        }

        public void Dispose()
        {
            uow.Dispose();
        }

        public User findUserBy(string champ)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {

            return uow.UserRepository.GetAll();
        }

        public User getById(string champ)
        {
            throw new NotImplementedException();
        }

        public User getById(int? id)
        {

            return uow.UserRepository.GetById(id);
        }

        public void SaveChange()
        {
            uow.Commit();
        }
    }
}
