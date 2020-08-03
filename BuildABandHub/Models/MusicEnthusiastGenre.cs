using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class MusicEnthusiastGenre
    {
        public int MusicEnthusiastGenreId { get; set; }
        //[Key,Column(Order = 0)]
        [ForeignKey("MusicEnthusiast")]
        public int MusicEnthusiastId { get; set; }
        //[Key,Column(Order = 1)]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        //public MusicEnthusiast MusicEnthusiast { get; set; }
        //public Genre Genre { get; set; }
    }
}
