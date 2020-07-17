using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuildABandHub.Data.Migrations
{
    public partial class AddedDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ea8ef3b-c298-4cc9-aaa2-db4068fd3fb9");

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Blues = table.Column<bool>(nullable: false),
                    Funk = table.Column<bool>(nullable: false),
                    Rock = table.Column<bool>(nullable: false),
                    Progressive = table.Column<bool>(nullable: false),
                    Metal = table.Column<bool>(nullable: false),
                    Punk = table.Column<bool>(nullable: false),
                    HipHopRap = table.Column<bool>(nullable: false),
                    Pop = table.Column<bool>(nullable: false),
                    ClassicRock = table.Column<bool>(nullable: false),
                    Electronic = table.Column<bool>(nullable: false),
                    Jazz = table.Column<bool>(nullable: false),
                    Classical = table.Column<bool>(nullable: false),
                    Other = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guitar = table.Column<bool>(nullable: false),
                    Drums = table.Column<bool>(nullable: false),
                    Vocals = table.Column<bool>(nullable: false),
                    Piano = table.Column<bool>(nullable: false),
                    Violin = table.Column<bool>(nullable: false),
                    HurdyGurdy = table.Column<bool>(nullable: false),
                    Saxophone = table.Column<bool>(nullable: false),
                    Other = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.InstrumentId);
                });

            migrationBuilder.CreateTable(
                name: "Musician",
                columns: table => new
                {
                    MusicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    YearsTogether = table.Column<int>(nullable: false),
                    BandMembers = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PracticePerWeek = table.Column<int>(nullable: false),
                    GigsPlayed = table.Column<int>(nullable: false),
                    GigsPerWeek = table.Column<int>(nullable: false),
                    Equipment = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    GenreId = table.Column<int>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musician", x => x.MusicianId);
                    table.ForeignKey(
                        name: "FK_Musician_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Musician_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Musician_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "InstrumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69d4dd71-e06f-497a-ad6a-93a9a4feef6a", "e45c8e50-028b-443e-8d20-0dd7fc6e5d61", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Musician_GenreId",
                table: "Musician",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Musician_IdentityUserId",
                table: "Musician",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Musician_InstrumentId",
                table: "Musician",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Musician");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69d4dd71-e06f-497a-ad6a-93a9a4feef6a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ea8ef3b-c298-4cc9-aaa2-db4068fd3fb9", "7c98f005-47ba-4034-a145-73437f9fc6e6", "Admin", "ADMIN" });
        }
    }
}
