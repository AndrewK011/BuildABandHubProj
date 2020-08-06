using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildABandHub.Data;
using BuildABandHub.Models;
using BuildABandHub.ViewModels;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;

namespace BuildABandHub.Controllers
{
    public class MusicEnthusiastsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public MusicEnthusiastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MusicEnthusiasts
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return View();
            }
            var musicEnthusiast = _context.MusicEnthusiasts
                .Include(m => m.Address)
                .FirstOrDefault(m => m.IdentityUserId == userId);
            if (musicEnthusiast == null)
            {
                return RedirectToAction("Create");
            }
            var applicationDbContext = _context.MusicEnthusiasts.Include(m => m.Address).Include(m => m.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MusicEnthusiasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicEnthusiast = await _context.MusicEnthusiasts
                .Include(m => m.Address)
                .Include(m => m.IdentityUser)
                .FirstOrDefaultAsync(m => m.MusicEnthusiastId == id);
            if (musicEnthusiast == null)
            {
                return NotFound();
            }

            return View(musicEnthusiast);
        }

        // GET: MusicEnthusiasts/Create
        public IActionResult Create()
        {
            var genre = _context.Genres.Select(g => new SelectListItem()
            {
                Text = g.TypeOfGenre,
                Value = g.GenreId.ToString()
            }).ToList();
            var vm = new MusicEnthusiastCreateViewModel()
            {
                Genre = genre
            };
            return View(vm);
        }

        // POST: MusicEnthusiasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicEnthusiastCreateViewModel musicEnthusiast)
        {
            MusicEnthusiastGenre musicEnthusiastGenre = new MusicEnthusiastGenre();

            if (ModelState.IsValid)
            {
                MusicEnthusiast newMusicEnthusiast = new MusicEnthusiast
                {
                    Username = musicEnthusiast.Username,
                    FirstName = musicEnthusiast.FirstName,
                    Address = musicEnthusiast.Address
                };
                newMusicEnthusiast.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(newMusicEnthusiast);
                await _context.SaveChangesAsync();
                var genreIds = musicEnthusiast.Genre.Where(g => g.Selected)
                                             .Select(v => v.Value);
                foreach (var id in genreIds)
                {
                    musicEnthusiastGenre.GenreId = int.Parse(id);
                    musicEnthusiastGenre.MusicEnthusiastId = newMusicEnthusiast.MusicEnthusiastId;
                    _context.Add(musicEnthusiastGenre);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = newMusicEnthusiast.MusicEnthusiastId });
            }
            return View(musicEnthusiast);
        }

        // GET: MusicEnthusiasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicEnthusiast = await _context.MusicEnthusiasts.FindAsync(id);
            if (musicEnthusiast == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", musicEnthusiast.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musicEnthusiast.IdentityUserId);
            return View(musicEnthusiast);
        }

        // POST: MusicEnthusiasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MusicEnthusiastId,Username,FirstName,AddressId,IdentityUserId")] MusicEnthusiast musicEnthusiast)
        {
            if (id != musicEnthusiast.MusicEnthusiastId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicEnthusiast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicEnthusiastExists(musicEnthusiast.MusicEnthusiastId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", musicEnthusiast.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musicEnthusiast.IdentityUserId);
            return View(musicEnthusiast);
        }

        // GET: MusicEnthusiasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicEnthusiast = await _context.MusicEnthusiasts
                .Include(m => m.Address)
                .Include(m => m.IdentityUser)
                .FirstOrDefaultAsync(m => m.MusicEnthusiastId == id);
            if (musicEnthusiast == null)
            {
                return NotFound();
            }

            return View(musicEnthusiast);
        }

        // POST: MusicEnthusiasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicEnthusiast = await _context.MusicEnthusiasts.FindAsync(id);
            _context.MusicEnthusiasts.Remove(musicEnthusiast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicEnthusiastExists(int id)
        {
            return _context.MusicEnthusiasts.Any(e => e.MusicEnthusiastId == id);
        }
    }
}
