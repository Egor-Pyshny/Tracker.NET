using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Passport
    {
        [Key]
        public string id { get; set; }
        public Address address { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string third_name { get; set; }
        public DateTime birthday { get; set; }
        public string gender { get; set; }
    }
}
