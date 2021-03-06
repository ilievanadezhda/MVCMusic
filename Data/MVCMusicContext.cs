using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCMusic.Areas.Identity.Data;
using MVCMusic.Models;

namespace MVCMusic.Data
{
    public class MVCMusicContext : IdentityDbContext<MVCMusicUser>
    {
        public MVCMusicContext (DbContextOptions<MVCMusicContext> options)
            : base(options)
        {
        }

        public DbSet<MVCMusic.Models.Song> Song { get; set; }

        public DbSet<MVCMusic.Models.Album> Album { get; set; }

        public DbSet<MVCMusic.Models.Artist> Artist { get; set; }

        public DbSet<MVCMusic.Models.SongArtist> SongArtist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
