using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Doctors
    {
        public int id { get; set; }
        public Hospitals hospital { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string third_name { get; set; }

        public List<Surgeries_graphic> surgeries_Graphics { get; set; } = new();
        public List<Surgeries_history> surgeries_History { get; set; } = new();
    }
}
