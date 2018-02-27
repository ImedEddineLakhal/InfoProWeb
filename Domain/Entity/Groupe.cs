using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
   public class Groupe
    {
        private int IdGroupe { get; set; }
        private String nom { get; set; }

        private String responsable { get; set; }

        public virtual ICollection<Employee> employees { get; set; }


    }
}
