using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.Data.Models
{
    public class Address
    {
        public int id {  get; set; }
        [MaxLength(6)]
        public string building {  get; set; }
        [MaxLength(6)]
        public string apartment { get; set; }
        public Street street { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
    }
}
