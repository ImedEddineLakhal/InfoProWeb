using ClassLibrary1.Repository;
using Data;
using Data.Repository;
using MyReports.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        private ReportContext dataContext;

        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;

        }
        protected ReportContext DataContext
        {
            get
            {
                return dataContext = dbFactory.DataContext;
            }
        }


        public void Commit()
        {
            DataContext.SaveChanges();
        }
        public void CommitAsync()
        {
            DataContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            dbFactory.Dispose();
        }

        private IAlerteRepository alerteRepository;
        public IAlerteRepository AlerteRepository
        {
            get { return alerteRepository = new AlerteRepository(dbFactory); }
        }


        private IUserRepository userRepository;

        public IUserRepository UserRepository
        {
            get { return userRepository = new UserRepository(dbFactory); }
        }

        private IEmployeeRepository employeeRepository;

        public IEmployeeRepository EmployeeRepository
        {
            get { return employeeRepository = new EmployeeRepository(dbFactory); }
        }

        private ITitreRepository titreRepository;

        public ITitreRepository TitreRepository
        {
            get { return titreRepository = new TitreRepository(dbFactory); }
        }

        private IGroupeRepository groupeRepository;

        public IGroupeRepository GroupeRepository
        {
            get { return groupeRepository = new GroupeRepository(dbFactory); }
        }

        private IIndicateurRepository indicateurRepository;

        public IIndicateurRepository IndicateurRepository
        {
            get { return indicateurRepository = new IndicateurRepository(dbFactory); }
        }

        private IPlaningRepository planingRepository;

        public IPlaningRepository PlaningRepository
        {
            get { return planingRepository = new PlaningRepository(dbFactory); }
        }


       
    }
}
