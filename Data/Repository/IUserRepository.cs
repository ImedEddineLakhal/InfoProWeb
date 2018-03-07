using Domain.Entity;
using MyReports.Data.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUserRepository: IRepositoryBase<User>
    {
         User getByLogin(string login) ;

    }
}
