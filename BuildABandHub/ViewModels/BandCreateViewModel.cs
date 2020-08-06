using BuildABandHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.ViewModels
{
    public class BandCreateViewModel
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
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Practices Per Week")]
        public int PracticesPerWeek { get; set; }
        [DisplayName("Gigs Played")]
        [Range(0, 10000)]
        public int GigsPlayed { get; set; }
        [DisplayName("Gigs Per Week")]
        public int GigsPerWeek { get; set; }
        [DisplayName("Select A Profile Picture")]
        public IFormFile Image { get; set; }
        public string VideoUrl { get; set; }
        public IList<SelectListItem> Genre { get; set; }
        public IList<SelectListItem> Instrument { get; set; }
    }
}
