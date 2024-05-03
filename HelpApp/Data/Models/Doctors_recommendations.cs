using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Doctors_recommendations
    {
        public int id { get; set; }
        public Patients patient { get; set; }
        public Doctors doctor { get; set; }
        [MaxLength(2000)]
        public string recommendations { get; set; }
    }
}
