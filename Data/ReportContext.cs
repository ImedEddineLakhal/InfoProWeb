using Data.CustumConvention;
using Domain.Entity;
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
        public DbSet<Alerte> alertes { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<User> users { get; set; }
        public DbSet<Planing> planings { get; set; }

        public DbSet<Titre> titres { get; set; }
        public DbSet<Indicateur> indicateurs { get; set; }

        public DbSet<Groupe> groupes { get; set; }

        public DbSet<AppointmentDiary> appointmentDiarys { get; set; }
        //public DbSet<Utils> utils { get; set; }
        //public DbSet<DiaryEvents> diaryEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(new DateTime2Convention());

        }

    }
}
