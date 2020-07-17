using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.Models
{
    public class Band
    {
        public int BandId { get; set; }
        public string BandName { get; set; }
        public int YearsTogether { get; set; }
        public string BandMembers { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Email { get; set; }
        public int PracticesPerWeek { get; set; }
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
