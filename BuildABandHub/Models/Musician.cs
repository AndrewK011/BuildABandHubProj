using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class Musician
    {
        public int MusicianId { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Username { get; set; }
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public int YearsTogether { get; set; }
        public string BandMembers { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Email { get; set; }
        public int PracticePerWeek { get; set; }
        public int GigsPlayed { get; set; }
        public int GigsPerWeek { get; set; }
        public string Equipment { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        [ForeignKey("Instruments")]
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
