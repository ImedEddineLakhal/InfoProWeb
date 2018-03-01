using Data;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsRPH
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportContext context = new ReportContext();
            List<Employee> employees = new List<Employee> { new Employee { Id = 1001, login = "imed" }, new Employee { Id = 1005, login = "monom" }, new Employee { Id = 1007, login = "tarek" }, new Employee { Id = 1008, login = "Sana" } };
            context.employees.AddRange(employees);
            context.SaveChanges();
            //List<Indicateur> indicateurs = new List<Indicateur> { new Indicateur { Id = 1001, appelEmis = 10 }, new Indicateur { Id = 1005, appelEmis =6 } };
            //context.indicateurs.AddRange(indicateurs);
            //context.SaveChanges();

        }
    }
}
