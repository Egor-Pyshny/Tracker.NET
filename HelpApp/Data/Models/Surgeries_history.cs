using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Surgeries_history
    {
        public int id { get; set; }
        public Surgery_reports surgery_Report { get; set; }
        public Hospitals hospital { get; set; }
        public Patients patient { get; set; }
        public DateTime date { get; set; }

        public List<Doctors> doctors { get; set; } = new();
    }
}
