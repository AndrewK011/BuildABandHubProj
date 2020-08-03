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
        //[Key, Column(Order = 0)]
        [ForeignKey("Band")]
        public int BandId { get; set; }
        //[Key, Column(Order = 1)]
        [ForeignKey("Musician")]
        public int MusicianId { get; set; }
        //public Band Band { get; set; }
        //public Musician Musician { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public bool Active { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
