using BuildABandHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.ViewModels
{
    public class MusicianCreateViewModel
    {
        public int MusicianId { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Username { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        [DisplayName("Practice Per Week")]
        public int PracticePerWeek { get; set; }
        [DisplayName("Gigs Played")]
        public int GigsPlayed { get; set; }
        [DisplayName("Max Gigs Per Week")]
        public int GigsPerWeek { get; set; }
        public string Equipment { get; set; }
        [DisplayName("Upload A Profile Picture")]
        public IFormFile Image { get; set; }
        public string VideoUrl { get; set; }
        public string Bio { get; set; }
        public string Influences { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [DisplayName("Genre")]
        [ForeignKey("MusicianGenre")]
        public int MusicianGenreId { get; set; }
        public MusicianGenre MusicianGenre { get; set; }
        public MusicianGenre MusicianInstrument { get; set; }
    }
}
