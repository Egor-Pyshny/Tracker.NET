using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Game_history
    {
        public int id { get; set; }
        public Patients patient { get; set; }
        public Levels level { get; set; }
        [MaxLength(150)]
        public string file_path { get; set; }
        public DateTime date { get; set; }
    }
}
