using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class BandGenre
    {
        public int BandGenreId { get; set; }
        //[Key, Column(Order = 0)]
        [ForeignKey("Band")]
        public int BandId { get; set; }
        //[Key, Column(Order = 1)]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        //public Band Band { get; set; }

        //public Genre Genre { get; set; }
    }
}
