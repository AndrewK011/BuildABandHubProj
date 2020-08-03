using Microsoft.EntityFrameworkCore.Migrations;

namespace BuildABandHub.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c503ceb-9586-4009-a722-ec25955e9ce8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "935a4a9a-609c-4820-a317-f30459d213db");

            migrationBuilder.DropColumn(
                name: "CommitmentLevel",
                table: "Bands");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Musicians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Bands",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a3cbd76-fc64-41e7-810d-7f6958e55ab4", "aa23177f-45c8-4e6b-a0ff-bd284359c806", "Musician", "MUSICIAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0438d512-012e-469f-84dc-7335437ad701", "25e31b22-ebe5-4005-b3ca-50040d99a708", "Music Enthusiast", "MUSIC ENTHUSIAST" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0438d512-012e-469f-84dc-7335437ad701");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a3cbd76-fc64-41e7-810d-7f6958e55ab4");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Bands");

            migrationBuilder.AddColumn<int>(
                name: "CommitmentLevel",
                table: "Bands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "935a4a9a-609c-4820-a317-f30459d213db", "50a7444b-379d-4cfe-867a-43ade702f35e", "Musician", "MUSICIAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c503ceb-9586-4009-a722-ec25955e9ce8", "752ae443-a255-4c1d-a85c-38162c0fe8a0", "Music Enthusiast", "MUSIC ENTHUSIAST" });
        }
    }
}
