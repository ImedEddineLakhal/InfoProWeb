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
        public int Id { get; set; }
        public String login { get; set; }

        public String password { get; set; }

        public String nom { get; set; }
        public String prenom { get; set; }

        public String role { get; set; }

        public String responsable { get; set; }

       


    }
}
