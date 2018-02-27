using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
   public class Titre
    {
        private int IdTitre { get; set; }

        public String libelle { get; set; }

        public virtual ICollection<Indicateur> indicateurs { get; set; }
        public virtual ICollection<Planing> planings { get; set; }

    }
}
