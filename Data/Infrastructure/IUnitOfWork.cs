using System;
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

        void CommitAsync();
        void Commit();

    }

}
