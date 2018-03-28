using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateDebut { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateFin { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string themeColor { get; set;}
        public virtual ICollection<Employee> employee { get; set; }
    }
}
