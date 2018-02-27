using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        private int IdPersonne { get; set; }
        private String login { get; set; }

        private String password { get; set; }

        private String nom { get; set; }
        private String prenom { get; set; }

        private String role { get; set; }

        private String responsable { get; set; }

       


    }
}
