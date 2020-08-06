using BuildABandHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.ViewModels
{
    public class BandIndexViewModel
    {
        public int BandId { get; set; }
        public string BandName { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string ImagePath { get; set; }
        public int GigsPlayed { get; set; }
        public int GigsPerWeek { get; set; }
        public List<Instrument> Instruments { get; set; }


    }
}
