using MVCMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.ViewModels
{
    public class ArtistsViewModel
    {
        public IList<Artist> Artists { get; set; }
        public string SearchName { get; set; }
        public string SearchSurname { get; set; }
    }
}
