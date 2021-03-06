﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;

namespace MyReports.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepositoryBaseAsynch<T> getRepository<T>() where T : class;
        // IContratRepository ContratRepository { get; }
        IGroupesEmployeesRepository GroupesEmployeesRepository { get; }
        IAlerteRepository AlerteRepository { get; }
        IUserRepository UserRepository { get; }

        IIndicateurRepository IndicateurRepository { get; }
        IPlaningRepository PlaningRepository { get; }

        ITitreRepository TitreRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        IGroupeRepository GroupeRepository { get; }
        IAttencanceHermesRepository AttendanceHermesRepository { get; }
        IAppelRepository AppelRepository { get; }
        IEventRepository EventRepository { get; }
        void CommitAsync();
        void Commit();

    }

}
