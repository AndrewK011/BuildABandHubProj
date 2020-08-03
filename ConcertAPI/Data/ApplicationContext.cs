using Microsoft.EntityFrameworkCore;
using ConcertAPI.Models;

namespace ConcertAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Concert>()
             .HasData(
                new Concert { ConcertId = 1, Artist = "Gojira", Venue = "The Eagles Ballroom", City = "Milwaukee", State = "WI", Lat = 43.038222, Long = -87.943192, Genre = "Metal", Date = "7/30/2020 7:00:00 PM"},
                new Concert { ConcertId = 2, Artist = "Gojira", Venue = "Huntington Bank Pavilion", City = "Chicago", State = "IL", Lat = 41.862908, Long = -87.608751, Genre = "Metal", Date = "7/31/2020 7:00:00 PM"},
                new Concert { ConcertId = 3, Artist = "Caligula's Horse", Venue = "Reggie's", City = "Chicago", State = "IL", Lat = 41.853991, Long = -87.626758, Genre = "Metal", Date = "8/30/2020 7:00:00 PM"},
                new Concert { ConcertId = 4, Artist = "Tigran Hamasyan", Venue = "Lincoln Hall", City = "Chicago", State = "IL", Lat = 41.925946, Long = -87.649837, Genre = "Jazz", Date = "9/07/2020 7:00:00 PM"},
                new Concert { ConcertId = 5, Artist = "Wardruna", Venue = "Auditorium Theatre", City = "Chicago", State = "IL", Lat = 41.875848, Long = -87.625113, Genre = "Neofolk", Date = "10/02/2020 7:00:00 PM"},
                new Concert { ConcertId = 6, Artist = "Jinjer", Venue = "Route 20", City = "Racine", State = "WI", Lat = 42.726099, Long = -87.957559, Genre = "Metal", Date = "10/26/2020 7:00:00 PM"},
                new Concert { ConcertId = 7, Artist = "Nothing More", Venue = "Route 20", City = "Racine", State = "WI", Lat = 42.726099, Long = -87.957559, Genre = "Rock", Date = "02/12/2021 7:00:00 PM"}
             );

        }

        public DbSet<Concert> Concerts { get; set; }
    }
}
