using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showroom.Infrastructure.Migrations
{
    public partial class AddGarage3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6501a46-a449-493a-bf61-9f33c81068c0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "GarageId", "LastName", "Password", "Username" },
                values: new object[] { new Guid("895aa13a-4eaf-4382-9d93-a646b9cf6929"), "ADMIN", new Guid("157d1dc4-1948-4a26-9891-8a5f5e76c9af"), "ADMINOV", "123456", "admin" });

            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("157d1dc4-1948-4a26-9891-8a5f5e76c9af"), new Guid("895aa13a-4eaf-4382-9d93-a646b9cf6929") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: new Guid("157d1dc4-1948-4a26-9891-8a5f5e76c9af"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("895aa13a-4eaf-4382-9d93-a646b9cf6929"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "GarageId", "LastName", "Password", "Username" },
                values: new object[] { new Guid("f6501a46-a449-493a-bf61-9f33c81068c0"), "ADMIN", new Guid("00000000-0000-0000-0000-000000000000"), "ADMINOV", "123456", "admin" });
        }
    }
}
