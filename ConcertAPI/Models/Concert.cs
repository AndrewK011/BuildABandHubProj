using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertAPI.Models
{
    public class Concert
    {
        public int ConcertId { get; set; }
        [Required(ErrorMessage ="Artist Required")]
        public string Artist { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Genre { get; set; }
        public string Date { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
