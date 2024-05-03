using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Appointment_records
    {
        public int id { get; set; }
        public Patients patient { get; set; }
        public Doctors doctor { get; set; }
        public Hospitals hospital { get; set; }
        public DateTime date { get; set; }
    }
}
