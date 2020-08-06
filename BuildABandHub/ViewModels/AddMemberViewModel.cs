using BuildABandHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildABandHub.ViewModels
{
    public class AddMemberViewModel
    {
        public int BandId { get; set; }
        public string BandName { get; set; }
        public string Username { get; set; }
        //public List<Musician> Musicians { get; set; }
    }
}
