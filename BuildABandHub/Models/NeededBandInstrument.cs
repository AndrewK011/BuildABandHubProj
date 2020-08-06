using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class NeededBandInstrument
    {
        public int NeededBandInstrumentId { get; set; }
        [ForeignKey("Band")]
        public int BandId { get; set; }
        [ForeignKey("Instrument")]
        public int InstrumentId { get; set; }
    }
}
