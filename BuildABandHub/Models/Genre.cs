using System.ComponentModel;

namespace BuildABandHub.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public bool Blues { get; set; }
        public bool Funk { get; set; }
        public bool Rock { get; set; }
        public bool Progressive { get; set; }
        public bool Metal { get; set; }
        public bool Punk { get; set; }
        [DisplayName("HipHop/Rap")]
        public bool HipHopRap { get; set; }
        public bool Pop { get; set; }
        [DisplayName("Classic Rock")]
        public bool ClassicRock { get; set; }
        public bool Electronic { get; set; }
        public bool Jazz { get; set; }
        public bool Classical { get; set; }
        public bool Other { get; set; }
    }
}