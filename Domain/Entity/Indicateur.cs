using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
   public class Indicateur
    {
        private int IdIndicateur { get; set; }
        private int agent { get; set; }

        private int CA { get; set; }  //NOMBRE DE contact argumenté

        private int accord { get; set; } // nombre des accords

        private int CNA { get; set; } // nombre des contacts non argumenté

        private int totAboutis { get; set; }

        private float tempsLog { get; set; }

        private float tempsComm { get; set; }

        private float tempsAtt { get; set; }

        private float tempsACW { get; set; } //temps ACW tems d'appel

        private float tempsPausePerso { get; set; }
        private float tempsPreview { get; set; }

        private float tempsPauseBrief { get; set; }

        private int responsable { get; set; } // a revoir

        private DateTime date { get; set; } // a revoir

        private int semaine { get; set; } // a revoir

        private String mois { get; set; } // a revoir
        private float tempsAcwHebdo { get; set; } // a revoir

        private float tempsAcwmensuel{ get; set; } // a revoir

        private float tempsAtthebdo { get; set; } // a revoir
        private float tempsAttmensuel { get; set; } // a revoir

        private float tempsPauseBriefHebdo { get; set; } // a revoir

        private float tempsPauseBriefMensuel{ get; set; } // a revoir

        private float tempsPausePersoHebdo { get; set; } // a revoir

        private float tempsPausePersoMensuel { get; set; } // a revoir


        private float tempsPrievewHebdo{ get; set; } // a revoir

        private float tempsPrievewMensuel { get; set; } // a revoir

        private int appelEmis { get; set; }

        private float tvente { get; set; }

        private float tventeHebdo { get; set; }
        private float tventeMensuel { get; set; }

        private float tAbs { get; set; }
        private float tAbsHebdo { get; set; }
        private float tAbsMensuel { get; set; }


        public int? employeeId { get; set; }

        [ForeignKey("employeeId")]
        public virtual Employee employee { get; set; }

        public int? alerteId { get; set; }

        [ForeignKey("alerteId")]
        public virtual Alerte alerte { get; set; }

        public int? titreId { get; set; }

        [ForeignKey("titreId")]
        public virtual Titre titre { get; set; }

    }
}
