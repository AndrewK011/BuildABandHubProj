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
                    Name = "Admin",
                    NormalizedName = "ADMIN" 
                }
            );
        }
        public DbSet<BuildABandHub.Models.Musician> Musicians { get; set; }
        public DbSet<BuildABandHub.Models.Musician> Bands { get; set; }
        public DbSet<BuildABandHub.Models.Musician> MusicEnthusiasts { get; set; }
        public DbSet<BuildABandHub.Models.Band> Band { get; set; }
        public DbSet<BuildABandHub.Models.MusicEnthusiast> MusicEnthusiast { get; set; }
    }
}
