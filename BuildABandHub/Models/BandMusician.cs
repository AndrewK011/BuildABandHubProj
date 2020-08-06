using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class BandMusician
    {
        public int BandMusicianId { get; set; }
        [ForeignKey("Band")]
        public int BandId { get; set; }
        [ForeignKey("Musician")]
        public int MusicianId { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public bool Active { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
