using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BuildABandHub.Models;

namespace BuildABandHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "Musician",
                    NormalizedName = "MUSICIAN"
                    
                },
                new IdentityRole
                {
                    Name = "Music Enthusiast",
                    NormalizedName = "MUSIC ENTHUSIAST"
                }
            );
        }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicEnthusiast> MusicEnthusiasts { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<BandMusician> BandMusicians { get; set; }
        public DbSet<MusicianGenre> MusicianGenres { get; set; }
        public DbSet<MusicEnthusiastGenre> MusicEnthusiastGenres { get; set; }
        public DbSet<BandGenre> BandGenres { get; set; }
        public DbSet<MusicianInstrument> MusicianInstruments { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Concert> Concert { get; set; }
        public DbSet<NeededBandInstrument> NeededBandInstruments { get; set; }
    }
}
