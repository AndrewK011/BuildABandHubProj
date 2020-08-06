using BuildABandHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class MusicEnthusiastCreateViewModel
    {
        public int MusicianId { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Username { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [EmailAddress]
        [DisplayName("Upload A Profile Picture")]
        public IFormFile Image { get; set; }
        [Url]
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [Required]
        public IList<SelectListItem> Genre { get; set; }
    }
}
