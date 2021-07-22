using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Genre { get; set; }
        [Display(Name = "File Path")]
        public string FilePath { get; set; }
        [Display(Name = "Album")]
        public int? AlbumId { get; set; }
        public Album? Album { get; set; }
        public ICollection<SongArtist> Artists { get; set; }
    }
}
