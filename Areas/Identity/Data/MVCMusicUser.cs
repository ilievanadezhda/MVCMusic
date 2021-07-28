using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVCMusic.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the MVCMusicUser class
    public class MVCMusicUser : IdentityUser
    {
        public int? ArtistId { get; set; }
    }
}
