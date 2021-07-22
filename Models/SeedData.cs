using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCMusic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusic.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCMusicContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<MVCMusicContext>>()))
            {
                // Look for any movies.
                if (context.Song.Any() || context.Album.Any() || context.Artist.Any())
                {
                    return; // DB has been seeded
                }
                context.Artist.AddRange(
                    new Artist { FirstName = "Olivia", LastName = "Rodrigo", BirthDate = DateTime.Parse("2003-2-20"), ProfilePicture = "olivia.jpg"},
                    new Artist { FirstName = "Ariana", LastName = "Grande", BirthDate = DateTime.Parse("1993-6-26"), ProfilePicture = "ariana.png" },
                    new Artist { FirstName = "Billie", LastName = "Eilish", BirthDate = DateTime.Parse("2001-12-18"), ProfilePicture = "billie.jpg" },
                    new Artist { FirstName = "Taylor", LastName = "Swift", BirthDate = DateTime.Parse("1989-12-13"), ProfilePicture = "taylor.jpg" },
                    new Artist { FirstName = "Miley", LastName = "Cyrus", BirthDate = DateTime.Parse("1992-11-23"), ProfilePicture = "miley.jpg" }
                    );
                context.SaveChanges();

                context.Album.AddRange(
                    new Album 
                    { 
                        Name = "Sour",
                        ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id,
                        Year = 2021,
                        RecordLabel = "Geffen Records",
                        AlbumCover = "sour.png"
                    },
                    new Album
                    {
                        Name = "Positions",
                        ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id,
                        Year = 2020,
                        RecordLabel = "Republic Records",
                        AlbumCover = "positions.png"
                    },
                    new Album
                    {
                        Name = "thank u, next",
                        ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id,
                        Year = 2019,
                        RecordLabel = "Republic Records",
                        AlbumCover = "thankunext.png"
                    },
                    new Album
                    {
                        Name = "Happier Than Ever",
                        ArtistId = context.Artist.Single(d => d.FirstName == "Billie" && d.LastName == "Eilish").Id,
                        Year = 2021,
                        RecordLabel = "Darkroom Interscope",
                        AlbumCover = "happierthanever.png"
                    },
                    new Album
                    {
                        Name = "Red",
                        ArtistId = context.Artist.Single(d => d.FirstName == "Taylor" && d.LastName == "Swift").Id,
                        Year = 2012,
                        RecordLabel = "Big Machine Records",
                        AlbumCover = "red.png"
                    },
                    new Album
                    {
                        Name = "Lover",
                        ArtistId = context.Artist.Single(d => d.FirstName == "Taylor" && d.LastName == "Swift").Id,
                        Year = 2019,
                        RecordLabel = "Republic Records",
                        AlbumCover = "lover.png"
                    }
                    );
                context.SaveChanges();
                context.Song.AddRange(
                    new Song { 
                        Name = "brutal", 
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/brutal.mp3"
                    },
                    new Song
                    {
                        Name = "traitor",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/traitor.mp3"
                    },
                    new Song
                    {
                        Name = "drivers licence",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/driverslicence.mp3"
                    },
                    new Song
                    {
                        Name = "1 step forward, 3 steps back",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/1sf3sb.mp3"
                    },
                    new Song
                    {
                        Name = "deja vu",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/dejavu.mp3"
                    },
                    new Song
                    {
                        Name = "good 4 u",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/good4u.mp3"
                    },
                    new Song
                    {
                        Name = "enough for you",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/enoughforyou.mp3"
                    },
                    new Song
                    {
                        Name = "happier",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/happier.mp3"
                    },
                    new Song
                    {
                        Name = "jealosy, jealosy",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/jealosy.mp3"
                    },
                    new Song
                    {
                        Name = "favourite crime",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/favouritecrime.mp3"
                    },
                    new Song
                    {
                        Name = "hope ur ok",
                        AlbumId = context.Album.Single(d => d.Name == "Sour").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/hopeurok.mp3"
                    },
                    new Song
                    {
                        Name = "imagine",
                        AlbumId = context.Album.Single(d => d.Name == "thank u, next").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/imagine.mp3"
                    },
                    new Song
                    {
                        Name = "needy",
                        AlbumId = context.Album.Single(d => d.Name == "thank u, next").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/needy.mp3"
                    },
                    new Song
                    {
                        Name = "NASA",
                        AlbumId = context.Album.Single(d => d.Name == "thank u, next").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/nasa.mp3"
                    },
                    new Song
                    {
                        Name = "bloodline",
                        AlbumId = context.Album.Single(d => d.Name == "thank u, next").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/bloodline.mp3"
                    },
                    new Song
                    {
                        Name = "fake smile",
                        AlbumId = context.Album.Single(d => d.Name == "thank u, next").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/fakesmile.mp3"
                    },
                    new Song
                    {
                        Name = "bad idea",
                        AlbumId = context.Album.Single(d => d.Name == "thank u, next").Id,
                        Genre = "Pop",
                        FilePath = "/tracks/badidea.mp3"
                    }
                    );
                context.SaveChanges();
                context.SongArtist.AddRange(
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "brutal").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "good 4 u").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "brutal").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "traitor").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "drivers licence").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "1 step forward, 3 steps back").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "deja vu").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "enough for you").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Taylor" && d.LastName == "Swift").Id, SongId = context.Song.Single(d => d.Name == "enough for you").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "happier").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "jealosy, jealosy").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "favourite crime").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Olivia" && d.LastName == "Rodrigo").Id, SongId = context.Song.Single(d => d.Name == "hope ur ok").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "imagine").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "needy").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "NASA").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "bloodline").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Miley" && d.LastName == "Cyrus").Id, SongId = context.Song.Single(d => d.Name == "bloodline").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "fake smile").Id },
                    new SongArtist { ArtistId = context.Artist.Single(d => d.FirstName == "Ariana" && d.LastName == "Grande").Id, SongId = context.Song.Single(d => d.Name == "bad idea").Id }
                    );
                context.SaveChanges();
            }
        }
    }
}
