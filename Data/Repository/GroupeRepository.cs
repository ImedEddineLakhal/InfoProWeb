﻿using Domain.Entity;
using MyReports.Data.Infrastracture;
using MyReports.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
   public  class GroupeRepository : RepositoryBase<Groupe>, IGroupeRepository
    {
        public GroupeRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
        ReportContext context = new ReportContext();
        public Groupe getByNom(string nom)
        {

            var groupe = context.groupes.FirstOrDefault(a => a.nom == nom);


            if (groupe != null)
            {

                return groupe;
            }


            else
            {
                return null;
            }
        }
        public Groupe getConatinsByNom(string nom)
        {

            var groupe = context.groupes.FirstOrDefault(a => a.nom.Contains(nom));


            if (groupe != null)
            {

                return groupe;
            }


            else
            {
                return null;
            }
        }
        public List<Groupe> getListByCorrespondance(String nomCorrespondance) {

            //from groupes in context.groupes
            //from employees in groupes.employees employees.groups.CityID == 1
            //select h;
            //var groupe=context.groupes
            //var blogs = from b in context.groupes
            //            where b.employees.("B")
            //            select b;
            //var result = (from m in context.groupes
            //              from b in m.employees
            //              where b.pseudoName.Contains(s)
            //              select new
            //              {
            //                  BandName = b.Name,
            //                  m.ID,
            //                  m.Name,
            //                  m.Description
            //              });
            //context.groupes.Where

            return null;
        }
        public List<Groupe> getListByCorrespondance(int IdCorrespondance)
        {

            //var groupe=context.groupes

            return null;
        }
    }
}
