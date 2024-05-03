using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Surgery_reports
    {
        public int id { get; set; }
        [MaxLength(45)]
        public string file_path { get; set; }
    }
}
