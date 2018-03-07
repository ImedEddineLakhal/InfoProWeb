using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Planing
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]

        public DateTime date { get; set; }
        [DataType(DataType.Date)]
        public DateTime heureArrivee { get; set; } // int dans la conception de départ
        [DataType(DataType.Date)]

        public DateTime dateDepart { get; set; }

        public int duree { get; set; }  // a revoir

 

        public virtual ICollection<Titre> titres { get; set; }



    }
}
