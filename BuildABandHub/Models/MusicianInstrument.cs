using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class MusicianInstrument
    {
        public int MusicianInstrumentId { get; set; }
        [ForeignKey("Musician")]
        public int MusicianId { get; set; }

        [ForeignKey("Instrument")]
        public int InstrumentId { get; set; }

    }
}
