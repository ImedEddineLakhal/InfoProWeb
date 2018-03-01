using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
  public class Employee
    {
        public int Id { get; set; }

        public String login { get; set; }

        public String password { get; set; }


        public String role { get; set; }
        public virtual ICollection<Groupe> Group { get; set; }
        public virtual ICollection<Indicateur> Indicateurs { get; set; }


    }



}
