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
        [ForeignKey("Band")]
        public int BandId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
    }
}
