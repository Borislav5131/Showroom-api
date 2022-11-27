using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showroom.Infrastructure.Migrations
{
    public partial class AddGarage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ef27578e-6f04-4a53-a04f-7bf61f9b50fc"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("054cbe3b-1c97-461c-8430-abe4a24f3f6c"), "ADMIN", "ADMINOV", "123456", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("054cbe3b-1c97-461c-8430-abe4a24f3f6c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("ef27578e-6f04-4a53-a04f-7bf61f9b50fc"), "ADMIN", "ADMINOV", "123456", "admin" });
        }
    }
}
