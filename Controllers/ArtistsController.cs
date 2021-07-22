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
    public class ArtistsController : Controller
    {
        private readonly MVCMusicContext _context;
        private readonly IHostingEnvironment webHostingEnvironment;

        public ArtistsController(MVCMusicContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            webHostingEnvironment = hostingEnvironment;
        }

        // GET: Artists
        public async Task<IActionResult> Index(string searchName, string searchSurname)
        {
            IQueryable<Artist> artists = _context.Artist.AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                artists = artists.Where(s => s.FirstName.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchSurname))
            {
                artists = artists.Where(x => x.LastName.Contains(searchSurname));
            }
            artists = artists.Include(a => a.Songs);
            var artistVM = new ArtistsViewModel
            {
                Artists = await artists.ToListAsync()
            };
            return View(artistVM);
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.Include(a => a.Songs).ThenInclude(a => a.Song).Include(a => a.Albums)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile pUrl, [Bind("Id,FirstName,LastName,BirthDate, ProfilePicture")] Artist artist)
        {
            ArtistsController uploadUrl = new ArtistsController(_context, webHostingEnvironment);
            artist.ProfilePicture = uploadUrl.UploadFile(pUrl);
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
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
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.Include(a => a.Songs).ThenInclude(a => a.Song).Include(a => a.Albums)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }

        public string UploadFile(IFormFile file)
        {
            string uniqueFileName = "proba";
            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "profilepicturess");
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
