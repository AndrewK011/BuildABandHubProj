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
    public class BandsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BandsController(ApplicationDbContext context,
                                    IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Bands
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bands.Include(b => b.Address);
            //NeededBandInstrument neededBandInstrument = new NeededBandInstrument();
            //var bandInstrument = _context.NeededBandInstruments.Select(i => i.InstrumentId).ToList();
            //List<Instrument> bandInstrumentList = new List<Instrument>();
            //foreach (var instrumentId in bandInstrument)
            //{
            //    bandInstrumentList.Add(_context.Instruments.Where(g => g.InstrumentId == instrumentId).FirstOrDefault());
            //}
            //var vm = new BandIndexViewModel()
            //{
            //    Instruments = bandInstrumentList,
            //    BandId = _context.Bands.Select(x => x.BandId).FirstOrDefault(),
            //    BandName = _context.Bands.Select(x => x.BandName).FirstOrDefault(),
            //    ImagePath = _context.Bands.Select(x => x.ImagePath).FirstOrDefault(),
            //    GigsPerWeek = _context.Bands.Select(x => x.GigsPerWeek).FirstOrDefault(),
            //    GigsPlayed = _context.Bands.Select(x => x.GigsPlayed).FirstOrDefault(),
            //    Address = _context.Bands.Select(x => x.Address).FirstOrDefault()
            //};
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands
                .Include(b => b.Address)
                .FirstOrDefaultAsync(m => m.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            var bandGenre = _context.BandGenres.Where(g => g.BandId == band.BandId).Select(x => x.GenreId).ToList();
            var bandInstrument = _context.NeededBandInstruments.Where(i => i.BandId == band.BandId).Select(i => i.InstrumentId).ToList();
            var bandMusician = _context.BandMusicians.Where(i => i.BandId == band.BandId).Select(i => i.MusicianId).ToList();

            List<Genre> bandGenreList = new List<Genre>();
            List<Instrument> bandInstrumentList = new List<Instrument>();
            List<Musician> bandMusicianList = new List<Musician>();

            foreach (var genreId in bandGenre)
            {
                bandGenreList.Add(_context.Genres.Where(g => g.GenreId == genreId).FirstOrDefault());
            }
            foreach (var instrumentId in bandInstrument)
            {
                bandInstrumentList.Add(_context.Instruments.Where(g => g.InstrumentId == instrumentId).FirstOrDefault());
            }
            foreach (var musicianId in bandMusician)
            {
                bandMusicianList.Add(_context.Musicians.Where(g => g.MusicianId == musicianId).FirstOrDefault());
            }
            var vm = new BandDetailsViewModel()
            {
                BandId = band.BandId,
                BandName = band.BandName,
                YearsTogether = band.YearsTogether,
                AddressId = band.AddressId,
                PracticesPerWeek = band.PracticesPerWeek,
                ImagePath = band.ImagePath,
                GigsPerWeek = band.GigsPerWeek,
                GigsPlayed = band.GigsPlayed,
                Email = band.Email,
                InstrumentsWanted = bandInstrumentList,
                Musicians = bandMusicianList,
                Genres = bandGenreList,
                VideoUrl = band.VideoUrl,
                Address = band.Address
            };
            foreach(var member in _context.BandMusicians.Where(b => b.BandId == band.BandId))
            {
                try
                {
                    if (_context.Musicians.Where(m => m.IdentityUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                                                                                    .FirstOrDefault()
                                                                                    .MusicianId == member.MusicianId && member.IsAuthorized)
                    {
                        return RedirectToAction("Profile", new { id });
                    }
                }
                catch
                {
                    return View();
                }

            }

            return View(vm);
        }

        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands
                .Include(b => b.Address)
                .FirstOrDefaultAsync(m => m.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            var bandGenre = _context.BandGenres.Where(g => g.BandId == band.BandId).Select(x => x.GenreId).ToList();
            var bandInstrument = _context.NeededBandInstruments.Where(i => i.BandId == band.BandId).Select(i => i.InstrumentId).ToList();
            var bandMusician = _context.BandMusicians.Where(i => i.BandId == band.BandId).Select(i => i.MusicianId).ToList();

            List<Genre> bandGenreList = new List<Genre>();
            List<Instrument> bandInstrumentList = new List<Instrument>();
            List<Musician> bandMusicianList = new List<Musician>();

            foreach (var genreId in bandGenre)
            {
                bandGenreList.Add(_context.Genres.Where(g => g.GenreId == genreId).FirstOrDefault());
            }
            foreach (var instrumentId in bandInstrument)
            {
                bandInstrumentList.Add(_context.Instruments.Where(g => g.InstrumentId == instrumentId).FirstOrDefault());
            }
            foreach (var musicianId in bandMusician)
            {
                bandMusicianList.Add(_context.Musicians.Where(g => g.MusicianId == musicianId).FirstOrDefault());
            }
            var vm = new BandDetailsViewModel()
            {
                BandId = band.BandId,
                BandName = band.BandName,
                YearsTogether = band.YearsTogether,
                AddressId = band.AddressId,
                PracticesPerWeek = band.PracticesPerWeek,
                ImagePath = band.ImagePath,
                GigsPerWeek = band.GigsPerWeek,
                GigsPlayed = band.GigsPlayed,
                Email = band.Email,
                InstrumentsWanted = bandInstrumentList,
                Musicians = bandMusicianList,
                Genres = bandGenreList,
                VideoUrl = band.VideoUrl,
                Address = band.Address
            };


            return View(vm);
        }

        public async Task<IActionResult> AddMembers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands
                .Include(b => b.Address)
                .FirstOrDefaultAsync(m => m.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            var vm = new AddMemberViewModel()
            {
                BandName = band.BandName,
                BandId = band.BandId
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddMembers(AddMemberViewModel addedMember)
        {
            var id = addedMember.BandId;
            if(addedMember.Username == null)
            {
                return NotFound();
            }

            var addedMemberId = _context.Musicians.Where(m => m.Username == addedMember.Username).FirstOrDefault().MusicianId;

            if (_context.BandMusicians.Where(b => b.MusicianId == addedMemberId).FirstOrDefault() == null)
            {
                BandMusician bandMusician = new BandMusician()
                {
                    MusicianId = addedMemberId,
                    BandId = addedMember.BandId,
                    Active = true,
                    IsAuthorized = false,
                    JoinDate = DateTime.Today
                };
                _context.Add(bandMusician);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id });
            }
            else if (_context.BandMusicians.Where(b => b.MusicianId == addedMemberId).FirstOrDefault().BandId != addedMember.BandId)
            {
                BandMusician bandMusician = new BandMusician()
                {
                    MusicianId = addedMemberId,
                    BandId = addedMember.BandId,
                    Active = true,
                    IsAuthorized = false,
                    JoinDate = DateTime.Today
                };
                _context.Add(bandMusician);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Bands/Create
        public IActionResult Create()
        {
            if(_context.Musicians.Where(m => m.IdentityUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault() == null)
            {
                return RedirectToAction("Index");
            }

            
            var genre = _context.Genres.Select(g => new SelectListItem()
            {
                Text = g.TypeOfGenre,
                Value = g.GenreId.ToString()
            }).ToList();
            var instrument = _context.Instruments.Select(g => new SelectListItem()
            {
                Text = g.TypeOfInstrument,
                Value = g.InstrumentId.ToString()
            }).ToList();
            var vm = new BandCreateViewModel()
            {
                Genre = genre,
                Instrument = instrument
            };
            return View(vm);
        }

        // POST: Bands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BandCreateViewModel band)
        {
            BandGenre bandGenre = new BandGenre();
            BandMusician bandMusician = new BandMusician();
            NeededBandInstrument bandInstrument = new NeededBandInstrument();

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (band.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + band.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    band.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Band newBand = new Band
                {
                    BandName = band.BandName,
                    Email = band.Email,
                    GigsPerWeek = band.GigsPerWeek,
                    GigsPlayed = band.GigsPlayed,
                    PracticesPerWeek = band.PracticesPerWeek,
                    Address = band.Address,
                    ImagePath = uniqueFileName,
                    VideoUrl = band.VideoUrl
                };
                _context.Add(newBand);
                await _context.SaveChangesAsync();

                bandMusician.BandId = newBand.BandId;
                bandMusician.MusicianId = _context.Musicians.Where(m => 
                                          m.IdentityUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault().MusicianId;
                bandMusician.IsAuthorized = true;
                bandMusician.Active = true;
                bandMusician.JoinDate = DateTime.Today;
                _context.BandMusicians.Add(bandMusician);
                var instrumentIds = band.Instrument.Where(g => g.Selected)
                                             .Select(v => v.Value);
                foreach (var id in instrumentIds)
                {
                    bandInstrument.InstrumentId = int.Parse(id);
                    bandInstrument.BandId = newBand.BandId;
                    _context.Add(bandInstrument);
                }
                    await _context.SaveChangesAsync();

                var genreIds = band.Genre.Where(g => g.Selected)
                                             .Select(v => v.Value);
                foreach (var id in genreIds)
                {
                    bandGenre.GenreId = int.Parse(id);
                    bandGenre.BandId = newBand.BandId;
                    _context.Add(bandGenre);
                }
                    await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = newBand.BandId });
            }
            //ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", band.AddressId);
            //ViewBag.BandGenreId = new SelectList(_context.MusicianGenres, "MusicianGenreId", "TypeOfGenre");
            return View(band);
        }

        // GET: Bands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands.FindAsync(id);
            if (band == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", band.AddressId);
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BandId,BandName,YearsTogether,AddressId,Email,CommitmentLevel,PracticesPerWeek,GigsPlayed,GigsPerWeek,ImagePath")] Band band)
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", band.AddressId);
            return View(band);
        }

        // GET: Bands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands
                .Include(b => b.Address)
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
            var band = await _context.Bands.FindAsync(id);
            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandExists(int id)
        {
            return _context.Bands.Any(e => e.BandId == id);
        }
    }
}
