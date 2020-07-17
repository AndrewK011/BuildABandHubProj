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
    public class MusiciansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusiciansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Musicians
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Musicians.Include(m => m.Genre).Include(m => m.IdentityUser).Include(m => m.Instrument);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Genre)
                .Include(m => m.IdentityUser)
                .Include(m => m.Instrument)
                .FirstOrDefaultAsync(m => m.MusicianId == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId");
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MusicianId,Username,FirstName,DOB,Gender,YearsTogether,BandMembers,City,State,Zip,Email,PracticePerWeek,GigsPlayed,GigsPerWeek,Equipment,ImagePath,GenreId,InstrumentId,IdentityUserId")] Musician musician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId", musician.GenreId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musician.IdentityUserId);
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId", musician.InstrumentId);
            return View(musician);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId", musician.GenreId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musician.IdentityUserId);
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId", musician.InstrumentId);
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MusicianId,Username,FirstName,DOB,Gender,YearsTogether,BandMembers,City,State,Zip,Email,PracticePerWeek,GigsPlayed,GigsPerWeek,Equipment,ImagePath,GenreId,InstrumentId,IdentityUserId")] Musician musician)
        {
            if (id != musician.MusicianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicianExists(musician.MusicianId))
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
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "GenreId", "GenreId", musician.GenreId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musician.IdentityUserId);
            ViewData["InstrumentId"] = new SelectList(_context.Set<Instrument>(), "InstrumentId", "InstrumentId", musician.InstrumentId);
            return View(musician);
        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Genre)
                .Include(m => m.IdentityUser)
                .Include(m => m.Instrument)
                .FirstOrDefaultAsync(m => m.MusicianId == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musician = await _context.Musicians.FindAsync(id);
            _context.Musicians.Remove(musician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicianExists(int id)
        {
            return _context.Musicians.Any(e => e.MusicianId == id);
        }
    }
}
