using Microsoft.AspNetCore.Mvc.Rendering;
using MVCMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.ViewModels
{
    public class SongsViewModel
    {
        public IList<Song> Songs { get; set; }
        public SelectList Genres { get; set; }
        public string SongGenre { get; set; }
        public string SearchName { get; set; }
    }
}
