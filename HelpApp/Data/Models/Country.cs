using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Country
    {
        public int id { get; set; }
        [MaxLength(45)]
        public string name { get; set; }
    }
}
