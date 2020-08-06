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
        [ForeignKey("MusicEnthusiast")]
        public int MusicEnthusiastId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
    }
}
