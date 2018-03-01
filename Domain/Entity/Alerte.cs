using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
   public class Alerte
    {
        public int Id { get; set; }

        public String titreAlerte { get; set; }

        public String description { get; set; }

        public DateTime dateCreation { get; set; }
        public String etatAlerte { get; set; }

        public String reponseAlerte { get; set; }

        public DateTime dateReponse { get; set; }

        public int? indicateurId { get; set; }

        [ForeignKey("indicateurId")]
        public virtual Indicateur indicateur { get; set; }

    }
}
