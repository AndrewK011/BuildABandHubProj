using Microsoft.EntityFrameworkCore.Migrations;

namespace ConcertAPI.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    ConcertId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(nullable: false),
                    Venue = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Long = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.ConcertId);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "Artist", "City", "Date", "Genre", "Lat", "Long", "State", "Venue" },
                values: new object[,]
                {
                    { 1, "Gojira", "Milwaukee", "7/30/2020 7:00:00 PM", "Metal", 43.038221999999998, -87.943191999999996, "WI", "The Eagles Ballroom" },
                    { 2, "Gojira", "Chicago", "7/31/2020 7:00:00 PM", "Metal", 41.862907999999997, -87.608750999999998, "IL", "Huntington Bank Pavilion" },
                    { 3, "Caligula's Horse", "Chicago", "8/30/2020 7:00:00 PM", "Metal", 41.853991000000001, -87.626757999999995, "IL", "Reggie's" },
                    { 4, "Tigran Hamasyan", "Chicago", "9/07/2020 7:00:00 PM", "Jazz", 41.925946000000003, -87.649837000000005, "IL", "Lincoln Hall" },
                    { 5, "Wardruna", "Chicago", "10/02/2020 7:00:00 PM", "Neofolk", 41.875847999999998, -87.625112999999999, "IL", "Auditorium Theatre" },
                    { 6, "Jinjer", "Racine", "10/26/2020 7:00:00 PM", "Metal", 42.726098999999998, -87.957559000000003, "WI", "Route 20" },
                    { 7, "Nothing More", "Racine", "02/12/2021 7:00:00 PM", "Rock", 42.726098999999998, -87.957559000000003, "WI", "Route 20" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
