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
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace BuildABandHub.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        string Baseurl = "https://localhost:44325/";
        List<Concert> Concerts = new List<Concert>();

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

        public async Task<IActionResult> LocalShows()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/concert/");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var ApiResponse = responseMessage.Content.ReadAsStringAsync().Result;

                    Concerts = JsonConvert.DeserializeObject<List<Concert>>(ApiResponse);
                }
            }
            return View(Concerts);
        }
        [HttpPost]
        public async Task<IActionResult> LocalShows(string id)
        {
            try
            {
                id.Trim();
                if (id.Contains(" "))
                {
                    id = id.Replace(" ", "/");

                }
                else if (id.Contains(","))
                {
                    id = id.Replace(",", "/");
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await client.GetAsync("api/concert/" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var ApiResponse = responseMessage.Content.ReadAsStringAsync().Result;

                        Concerts = JsonConvert.DeserializeObject<List<Concert>>(ApiResponse);
                    }
                }
                return View(Concerts);
            }
            catch
            {
                return View();
            }
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

            var musicianGenre = _context.MusicianGenres.Where(g => g.MusicianId == musician.MusicianId).Select(x => x.GenreId).ToList();
            var musicianInstrument = _context.MusicianInstruments.Where(i => i.MusicianId == musician.MusicianId).Select(i => i.InstrumentId).ToList();
            var musicianBand = _context.BandMusicians.Where(i => i.BandId == musician.MusicianId).Select(i => i.MusicianId).ToList();

            List<Genre> musicianGenreList = new List<Genre>();
            List<Instrument> musicianInstrumentList = new List<Instrument>();
            List<Band> musicianBandList = new List<Band>();

            foreach (var genreId in musicianGenre)
            {
                musicianGenreList.Add(_context.Genres.Where(g => g.GenreId == genreId).FirstOrDefault());
            }
            foreach (var instrumentId in musicianInstrument)
            {
                musicianInstrumentList.Add(_context.Instruments.Where(g => g.InstrumentId == instrumentId).FirstOrDefault());
            }
            foreach (var bandId in musicianBand)
            {
                musicianBandList.Add(_context.Bands.Where(g => g.BandId == bandId).FirstOrDefault());
            }
            var vm = new MusicianDetailsViewModel()
            {
                MusicianId = musician.MusicianId,
                Username = musician.Username,
                FirstName = musician.FirstName,
                DOB = musician.DOB,
                AddressId = musician.AddressId,
                PracticePerWeek = musician.PracticePerWeek,
                ImagePath = musician.ImagePath,
                GigsPerWeek = musician.GigsPerWeek,
                GigsPlayed = musician.GigsPlayed,
                Bio = musician.Bio,
                Influences = musician.Influences,
                Equipment = musician.Equipment,
                Gender = musician.Gender,
                Email = musician.Email,
                Instruments = musicianInstrumentList,
                Bands = musicianBandList,
                Genres = musicianGenreList,
                VideoUrl = musician.VideoUrl,
                Address = musician.Address
            };

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (musician == null)
            {
                return NotFound();
            }
            else if(musician.IdentityUserId == userId)
            {
                return RedirectToAction("Profile", new { id });
            }

            return View(vm);
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

            var musicianGenre = _context.MusicianGenres.Where(g => g.MusicianId == musician.MusicianId).Select(x => x.GenreId).ToList();
            var musicianInstrument = _context.MusicianInstruments.Where(i => i.MusicianId == musician.MusicianId).Select(i => i.InstrumentId).ToList();
            var musicianBand = _context.BandMusicians.Where(i => i.BandId == musician.MusicianId).Select(i => i.MusicianId).ToList();

            List<Genre> musicianGenreList = new List<Genre>();
            List<Instrument> musicianInstrumentList = new List<Instrument>();
            List<Band> musicianBandList = new List<Band>();

            foreach (var genreId in musicianGenre)
            {
                musicianGenreList.Add(_context.Genres.Where(g => g.GenreId == genreId).FirstOrDefault());
            }
            foreach (var instrumentId in musicianInstrument)
            {
                musicianInstrumentList.Add(_context.Instruments.Where(g => g.InstrumentId == instrumentId).FirstOrDefault());
            }
            foreach (var bandId in musicianBand)
            {
                musicianBandList.Add(_context.Bands.Where(g => g.BandId == bandId).FirstOrDefault());
            }
            var vm = new MusicianDetailsViewModel()
            {
                MusicianId = musician.MusicianId,
                Username = musician.Username,
                FirstName = musician.FirstName,
                DOB = musician.DOB,
                AddressId = musician.AddressId,
                PracticePerWeek = musician.PracticePerWeek,
                ImagePath = musician.ImagePath,
                GigsPerWeek = musician.GigsPerWeek,
                GigsPlayed = musician.GigsPlayed,
                Bio = musician.Bio,
                Influences = musician.Influences,
                Equipment = musician.Equipment,
                Gender = musician.Gender,
                Email = musician.Email,
                Instruments = musicianInstrumentList,
                Bands = musicianBandList,
                Genres = musicianGenreList,
                VideoUrl = musician.VideoUrl,
                Address = musician.Address
            };

            if (musician == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
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
            var vm = new MusicianCreateViewModel()
            {
                Genre = genre,
                Instrument = instrument
            };
            return View(vm);
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicianCreateViewModel musician)
        {
            MusicianGenre musicianGenre = new MusicianGenre();
            MusicianInstrument musicianInstrument = new MusicianInstrument();

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
                var instrumentIds = musician.Instrument.Where(g => g.Selected)
                                             .Select(v => v.Value);
                foreach (var id in instrumentIds)
                {
                    musicianInstrument.InstrumentId = int.Parse(id);
                    musicianInstrument.MusicianId = newMusician.MusicianId;
                    _context.Add(musicianInstrument);
                }
                await _context.SaveChangesAsync();

                var genreIds = musician.Genre.Where(g => g.Selected)
                                             .Select(v => v.Value);
                foreach(var id in genreIds)
                {
                    musicianGenre.GenreId = int.Parse(id);
                    musicianGenre.MusicianId = newMusician.MusicianId;
                    _context.Add(musicianGenre);
                }

                    await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = newMusician.MusicianId});
            }
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
