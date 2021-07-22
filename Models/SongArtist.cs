using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.Models
{
    public class SongArtist
    {
        public int Id { get; set; }
        [Display(Name = "Song")]
        public int SongId { get; set; }
        public Song Song { get; set; }
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
