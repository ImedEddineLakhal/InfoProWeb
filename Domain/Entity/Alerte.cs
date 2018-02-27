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
        public int IdAlerte { get; set; }

        private String titreAlerte { get; set; }

        private String description { get; set; }

        private DateTime dateCreation { get; set; }
        private String etatAlerte { get; set; }

        private String reponseAlerte { get; set; }

        private DateTime dateReponse { get; set; }

        public int? indicateurId { get; set; }

        [ForeignKey("indicateurId")]
        public virtual Indicateur indicateur { get; set; }

    }
}
