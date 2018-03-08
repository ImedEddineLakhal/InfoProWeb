using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public interface IUserService:IDisposable
    {
        void Add(User user);

        void Delete(User user);

        void SaveChange();
        User findUserBy(String champ);
        User getById(int? id);
        User getBylogin(String champ);
        IEnumerable<User> GetAll();

        void Dispose();
        User getByTempSortie(string login);
        
    }
}
