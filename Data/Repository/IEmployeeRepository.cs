using Domain.Entity;
using MyReports.Data.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
   public interface IEmployeeRepository: IRepositoryBase<Employee>
    {
        Employee getByLoginUser(string login);
        Employee getByLogin(string login);

        Employee getByIdHermes(int idHermes);
    }
}
