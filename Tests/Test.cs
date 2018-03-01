using Data;
using Domain.Entity;
using MyFinance.Data.Infrastructure;
using MyReports.Data.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Test
    {
       
      
        [Test]
        public void AddEmployee()

        {
            ReportContext context = new ReportContext();

            Employee employee = new Employee { Id = 8520, login = "salah" };
            context.employees.Add(employee);
            context.SaveChanges();


        }
        [Test]
        public void DeleteEmployee() {
            ReportContext context1 = new ReportContext();
            Employee employee = new Employee();
             employee = context1.employees.Find(7);
            //Employee employee1 = uow.EmployeeRepository.GetById(6);
            //Employee employee2 = uow.EmployeeRepository.GetById(7);
            //Employee employee3 = uow.EmployeeRepository.GetById(8);
            //Employee employee4 = uow.EmployeeRepository.GetById(9);
            context1.employees.Remove(employee);
            context1.SaveChanges();

        }
        [Test]
        public void update() {

            ReportContext context1 = new ReportContext();
            DatabaseFactory dbfactory = new DatabaseFactory();
            UnitOfWork uow = new UnitOfWork(dbfactory);
            Employee employee = new Employee();
            employee = uow.EmployeeRepository.GetById(6);
            employee.login = "souhaiel";
            //Employee employee1 = uow.EmployeeRepository.GetById(6);
            //Employee employee2 = uow.EmployeeRepository.GetById(7);
            //Employee employee3 = uow.EmployeeRepository.GetById(8);
            //Employee employee4 = uow.EmployeeRepository.GetById(9);
            uow.EmployeeRepository.Update(employee);
            uow.Commit();

        }
    }
}
