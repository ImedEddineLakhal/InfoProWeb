using Domain.Entity;
using MyReports.Data.Infrastracture;
using MyReports.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
        ReportContext context = new ReportContext();

        public List<Groupe> getListByCorrespondance(String nomCorrespondance)
        {

            //var groupe=context.groupes
            //var blogs = from b in context.groupes
            //            where b.employees.("B")
            //            select b;
            //context.groupes.Where
           
            return null;
        }
        public Employee getByLoginUser(string login)
        {

            var employee = context.employees.FirstOrDefault(a => a.userLogin == login);


            if (employee != null)
            {

                return employee;
            }


            else
            {
                return null;
            }
        }
        public Employee getByLogin(string login)
        {
            var employee = context.employees.FirstOrDefault(a => a.userName == login);


            if (employee != null)
            {

                return employee;
            }


            else
            {
                return null;
            }
        }
    }
}
