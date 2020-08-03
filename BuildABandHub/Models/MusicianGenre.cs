using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class MusicianGenre
    {
        public int MusicianGenreId { get; set; }
        [ForeignKey("Musician")]
        public int MusicianId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
    }
}
