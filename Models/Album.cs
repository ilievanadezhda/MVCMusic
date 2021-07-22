using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.Models
{
    public class Album
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public int Year { get; set; }
        [Display(Name = "Record Label")]
        public string RecordLabel { get; set; }
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set;}
        public ICollection<Song> Songs { get; set; }

        [Display(Name = "Album Cover")]
        public string? AlbumCover { get; set; }
    }
}
