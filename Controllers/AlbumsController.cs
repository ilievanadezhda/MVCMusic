using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMusic.Data;
using MVCMusic.Models;
using MVCMusic.ViewModels;

namespace MVCMusic.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MVCMusicContext _context;
        private readonly IHostingEnvironment webHostingEnvironment;

        public AlbumsController(MVCMusicContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            webHostingEnvironment = hostingEnvironment;
        }

        // GET: Albums
        public async Task<IActionResult> Index(string searchName, string searchRecordLabel, string searchArtist)
        {
            IQueryable<Album> albums = _context.Album.AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                albums = albums.Where(s => s.Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchRecordLabel))
            {
                albums = albums.Where(x => x.RecordLabel.Contains(searchRecordLabel));
            }
            if (!string.IsNullOrEmpty(searchArtist))
            {
                albums = albums.Where(x => x.Artist.FirstName.Contains(searchArtist) || x.Artist.LastName.Contains(searchArtist));
            }
            albums = albums.Include(a => a.Artist).Include(a => a.Songs);
            var albumVM = new AlbumsViewModel
            {
                Albums = await albums.ToListAsync()
            };
            return View(albumVM);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist).Include(a => a.Songs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "Id", "FullName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile pUrl, [Bind("Id,Name,Year,RecordLabel,ArtistId, AlbumCover")] Album album)
        {
            AlbumsController uploadUrl = new AlbumsController(_context, webHostingEnvironment);
            album.AlbumCover = uploadUrl.UploadFile(pUrl);
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "Id", "FullName", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "Id", "FullName", album.ArtistId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile pUrl, [Bind("Id,Name,Year,RecordLabel,ArtistId, AlbumCover")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }
            AlbumsController uploadUrl = new AlbumsController(_context, webHostingEnvironment);
            album.AlbumCover = uploadUrl.UploadFile(pUrl);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "Id", "FullName", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist).Include(a => a.Songs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Album.FindAsync(id);
            _context.Album.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.Id == id);
        }

        public string UploadFile(IFormFile file)
        {
            string uniqueFileName = "proba";
            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "covers");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            //uniqueFileName = "/covers/" + uniqueFileName;
            return uniqueFileName;
        }
    }
}
