using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entity
{
    public class AppointmentDiary
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int SomeImportantKey { get; set; }
        public System.DateTime DateTimeScheduled { get; set; }
        public int AppointmentLength { get; set; }
        public int StatusENUM { get; set; }
    }
}