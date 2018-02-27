using Data.CustumConvention;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReportContext : DbContext
    {
        public ReportContext() : base("name=ReportDBConnexion")
        {
            //Database.SetInitializer<MyAlfrescoContext>(new MyAlfrescoContextInitialiazor());

        }

       // public DbSet<Contrat> Contrats { get; set; }
   


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(new DateTime2Convention());

        }


    }
}
