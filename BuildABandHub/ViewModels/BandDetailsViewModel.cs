using BuildABandHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.ViewModels
{
    public class BandDetailsViewModel
    {
        public int BandId { get; set; }
        [Required]
        [DisplayName("Band Name")]
        public string BandName { get; set; }
        [DisplayName("Years Together")]
        public int YearsTogether { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        [DisplayName("Practices Per Week")]
        public int PracticesPerWeek { get; set; }
        [DisplayName("Gigs Played")]
        public int GigsPlayed { get; set; }
        [DisplayName("Gigs Per Week")]
        public int GigsPerWeek { get; set; }
        [DisplayName("Select A Profile Picture")]
        public string ImagePath { get; set; }
        public string VideoUrl { get; set; }
        public List<Musician> Musicians { get; set; }
        public List<Instrument> InstrumentsWanted { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
