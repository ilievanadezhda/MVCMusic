using MVCMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.ViewModels
{
    public class AlbumsViewModel
    {
        public IList<Album> Albums { get; set; }
        public string SearchName { get; set; }
        public string SearchRecordLabel { get; set; }
        public string SearchArtist { get; set; }
    }
}
