using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Planing
    {
        private int IdPlaning { get; set; }

        private DateTime date { get; set; }

        private DateTime heureArrivee { get; set; } // int dans la conception de départ

        private DateTime dateDepart { get; set; }

        private int duree { get; set; }  // a revoir

 

        public virtual ICollection<Titre> titres { get; set; }



    }
}
