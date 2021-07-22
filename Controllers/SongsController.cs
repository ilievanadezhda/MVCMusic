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
    public class SongsController : Controller
    {
        private readonly MVCMusicContext _context;
        private readonly IHostingEnvironment webHostingEnvironment;

        public SongsController(MVCMusicContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            webHostingEnvironment = hostingEnvironment;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string songGenre, string searchName)
        {
            IQueryable<Song> songs = _context.Song.AsQueryable();
            IQueryable<string> genreQuery = _context.Song.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct();
            if (!string.IsNullOrEmpty(searchName))
            {
                songs = songs.Where(s => s.Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(songGenre))
            {
                songs = songs.Where(x => x.Genre == songGenre);
            }
            songs = songs.Include(m => m.Album).Include(m => m.Artists).ThenInclude(m => m.Artist);
            var songVM = new SongsViewModel
            {
                Genres = new SelectList(await genreQuery.ToListAsync()),
                Songs = await songs.ToListAsync()
            };
            return View(songVM);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile pUrl, [Bind("Id,Name,Genre,FilePath,AlbumId")] Song song)
        {
            //SongsController uploadUrl = new SongsController(_context, webHostingEnvironment);
            song.FilePath = UploadFile(pUrl);

            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name", song.AlbumId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile pUrl, [Bind("Id,Name,Genre,FilePath,AlbumId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            SongsController uploadUrl = new SongsController(_context, webHostingEnvironment);
            song.FilePath = uploadUrl.UploadFile(pUrl);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            _context.Song.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }

        public string UploadFile(IFormFile file)
        {
            string uniqueFileName = "proba";
            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "tracks");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            uniqueFileName = "/tracks/" + uniqueFileName;
            return uniqueFileName;
        }
    }
}
