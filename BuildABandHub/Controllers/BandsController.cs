using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildABandHub.Data;
using BuildABandHub.Models;

namespace BuildABandHub.Controllers
{
    public class BandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bands
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Band.Include(b => b.Genre).Include(b => b.IdentityUser).Include(b => b.Instrument);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Band
                .Include(b => b.Genre)
                .Include(b => b.IdentityUser)
                .Include(b => b.Instrument)
                .FirstOrDefaultAsync(m => m.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
        }

        // GET: Bands/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId");
            return View();
        }

        // POST: Bands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BandId,BandName,YearsTogether,BandMembers,City,State,Zip,Email,PracticesPerWeek,GigsPlayed,GigsPerWeek,Equipment,ImagePath,GenreId,InstrumentId,IdentityUserId")] Band band)
        {
            if (ModelState.IsValid)
            {
                _context.Add(band);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId", band.GenreId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", band.IdentityUserId);
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId", band.InstrumentId);
            return View(band);
        }

        // GET: Bands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Band.FindAsync(id);
            if (band == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId", band.GenreId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", band.IdentityUserId);
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId", band.InstrumentId);
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BandId,BandName,YearsTogether,BandMembers,City,State,Zip,Email,PracticesPerWeek,GigsPlayed,GigsPerWeek,Equipment,ImagePath,GenreId,InstrumentId,IdentityUserId")] Band band)
        {
            if (id != band.BandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(band);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandExists(band.BandId))
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
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId", band.GenreId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", band.IdentityUserId);
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId", band.InstrumentId);
            return View(band);
        }

        // GET: Bands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Band
                .Include(b => b.Genre)
                .Include(b => b.IdentityUser)
                .Include(b => b.Instrument)
                .FirstOrDefaultAsync(m => m.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var band = await _context.Band.FindAsync(id);
            _context.Band.Remove(band);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandExists(int id)
        {
            return _context.Band.Any(e => e.BandId == id);
        }
    }
}
