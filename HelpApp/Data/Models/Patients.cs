using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Patients
    {
        public int id { get; set; }
        public Passport passport { get; set; }
        [MaxLength(45)]
        public string email { get; set; }
        public Doctors doctor { get; set; }
        public List<Diagnosis> diagnoses { get; set; } = new();
    }
}
