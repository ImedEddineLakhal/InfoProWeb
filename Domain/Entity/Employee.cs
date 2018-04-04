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
        public int Id { get; set; } //this is a property not a variable

        public int IdHermes { get; set; }

        public String userName { get; set; }

        public String pseudoName { get; set; }

        public String IdAD { get; set; }

        public String role { get; set; }
        public String Activite { get; set; }
        public virtual ICollection<GroupesEmployees> employeesGroupes { get; set; }
        public virtual ICollection<Indicateur> Indicateurs { get; set; }

        public virtual ICollection<Event> events { get; set; }

        public String userLogin { get; set; }

        public int? userId { get; set; }

        [ForeignKey("userId")]
        public virtual User user { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public byte[] Content { get; set; }

        public String ContentType { get; set; }

        public int getIdHermes()
        {
            return IdHermes;
        }
    }



}
