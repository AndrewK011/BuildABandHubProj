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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Security.Claims;

namespace BuildABandHub.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MusiciansController(ApplicationDbContext context,
                                    IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Musicians
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return RedirectToAction("NewUser");
            }
            var musician = _context.Musicians
                .Include(m => m.Address)
                .Include(m => m.IdentityUser)
                .FirstOrDefault(m => m.IdentityUserId == userId);
            if (musician == null)
            {
                return RedirectToAction("Create");
            }
            var applicationDbContext = _context.Musicians.Include(m => m.Address).Include(m => m.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult NewUser()
        {
            return View();
        }

        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Address)
                .Include(m => m.IdentityUser)
                .FirstOrDefaultAsync(m => m.MusicianId == id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (musician == null)
            {
                return NotFound();
            }
            else if(musician.IdentityUserId == userId)
            {
                return RedirectToAction("Profile", new { id });
            }

            return View(musician);
        }

        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Address)
                .Include(m => m.IdentityUser)
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicianCreateViewModel musician)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(musician.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + musician.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    musician.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Musician newMusician = new Musician
                {
                    Username = musician.Username,
                    FirstName = musician.FirstName,
                    DOB = musician.DOB,
                    Gender = musician.Gender,
                    Email = musician.Email,
                    Influences = musician.Influences,
                    Equipment = musician.Equipment,
                    GigsPerWeek = musician.GigsPerWeek,
                    GigsPlayed = musician.GigsPlayed,
                    PracticePerWeek = musician.PracticePerWeek,
                    Bio = musician.Bio,
                    Address = musician.Address,
                    ImagePath = uniqueFileName,
                    VideoUrl = musician.VideoUrl
                    
                };
                newMusician.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(newMusician);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = newMusician.MusicianId});
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", musician.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musician.IdentityUserId);
            ViewBag.MusicianGenreId = new SelectList(_context.MusicianGenres, "MusicianGenreId", "TypeOfGenre");
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", musician.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musician.IdentityUserId);
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Musician musician)
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", musician.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", musician.IdentityUserId);
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
                .Include(m => m.Address)
                .Include(m => m.IdentityUser)
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
