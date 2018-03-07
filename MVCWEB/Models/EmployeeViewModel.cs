using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWEB.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public int IdHermes { get; set; }

        public String userName { get; set; }

        public String pseudoName { get; set; }

        public int IdAD { get; set; }

        public String role { get; set; }
        public String Activite { get; set; }
        public virtual ICollection<Groupe> Group { get; set; }
        public virtual ICollection<Indicateur> Indicateurs { get; set; }
        public int? userId { get; set; }

        [ForeignKey("userId")]
        public virtual User user { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public List<SelectListItem> utilisateurs = new List<SelectListItem>();

        public String userLogin { get; set; }


    }
}