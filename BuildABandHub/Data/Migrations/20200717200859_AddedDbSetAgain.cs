using Microsoft.EntityFrameworkCore.Migrations;

namespace BuildABandHub.Data.Migrations
{
    public partial class AddedDbSetAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musician_Genre_GenreId",
                table: "Musician");

            migrationBuilder.DropForeignKey(
                name: "FK_Musician_AspNetUsers_IdentityUserId",
                table: "Musician");

            migrationBuilder.DropForeignKey(
                name: "FK_Musician_Instrument_InstrumentId",
                table: "Musician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musician",
                table: "Musician");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69d4dd71-e06f-497a-ad6a-93a9a4feef6a");

            migrationBuilder.RenameTable(
                name: "Musician",
                newName: "Musicians");

            migrationBuilder.RenameIndex(
                name: "IX_Musician_InstrumentId",
                table: "Musicians",
                newName: "IX_Musicians_InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Musician_IdentityUserId",
                table: "Musicians",
                newName: "IX_Musicians_IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Musician_GenreId",
                table: "Musicians",
                newName: "IX_Musicians_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musicians",
                table: "Musicians",
                column: "MusicianId");

            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    BandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BandName = table.Column<string>(nullable: true),
                    YearsTogether = table.Column<int>(nullable: false),
                    BandMembers = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PracticesPerWeek = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_Bands", x => x.BandId);
                    table.ForeignKey(
                        name: "FK_Bands_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bands_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bands_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "InstrumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicEnthusiasts",
                columns: table => new
                {
                    MusicEnthusiastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicEnthusiasts", x => x.MusicEnthusiastId);
                    table.ForeignKey(
                        name: "FK_MusicEnthusiasts_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicEnthusiasts_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a412d3a-b301-469b-8b7c-e9dafe2b0daf", "ba5c7b6c-63a4-4bde-a226-ad7a372c97d1", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Bands_GenreId",
                table: "Bands",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_IdentityUserId",
                table: "Bands",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_InstrumentId",
                table: "Bands",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicEnthusiasts_GenreId",
                table: "MusicEnthusiasts",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicEnthusiasts_IdentityUserId",
                table: "MusicEnthusiasts",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicians_Genre_GenreId",
                table: "Musicians",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musicians_AspNetUsers_IdentityUserId",
                table: "Musicians",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Musicians_Instrument_InstrumentId",
                table: "Musicians",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "InstrumentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicians_Genre_GenreId",
                table: "Musicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Musicians_AspNetUsers_IdentityUserId",
                table: "Musicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Musicians_Instrument_InstrumentId",
                table: "Musicians");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "MusicEnthusiasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musicians",
                table: "Musicians");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a412d3a-b301-469b-8b7c-e9dafe2b0daf");

            migrationBuilder.RenameTable(
                name: "Musicians",
                newName: "Musician");

            migrationBuilder.RenameIndex(
                name: "IX_Musicians_InstrumentId",
                table: "Musician",
                newName: "IX_Musician_InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Musicians_IdentityUserId",
                table: "Musician",
                newName: "IX_Musician_IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Musicians_GenreId",
                table: "Musician",
                newName: "IX_Musician_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musician",
                table: "Musician",
                column: "MusicianId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69d4dd71-e06f-497a-ad6a-93a9a4feef6a", "e45c8e50-028b-443e-8d20-0dd7fc6e5d61", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Musician_Genre_GenreId",
                table: "Musician",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musician_AspNetUsers_IdentityUserId",
                table: "Musician",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Musician_Instrument_InstrumentId",
                table: "Musician",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "InstrumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
