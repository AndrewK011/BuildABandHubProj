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
    public class MusicianDetailsViewModel
    {
        public int MusicianId { get; set; }
        public string Username { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        [DisplayName("Practices Per Week")]
        public int PracticePerWeek { get; set; }
        [DisplayName("Gigs Played")]
        public int GigsPlayed { get; set; }
        [DisplayName("Gigs Per Week")]
        public int GigsPerWeek { get; set; }
        [DisplayName("Select A Profile Picture")]
        public string Equipment { get; set; }
        [DisplayName("Upload A Profile Picture")]
        public string ImagePath { get; set; }
        [DisplayName("Video URL")]
        public string VideoUrl { get; set; }
        public string Bio { get; set; }
        public string Influences { get; set; }
        public List<Band> Bands { get; set; }
        public List<Instrument> Instruments { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
