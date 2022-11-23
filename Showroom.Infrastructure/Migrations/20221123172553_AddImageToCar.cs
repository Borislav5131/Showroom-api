using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showroom.Infrastructure.Migrations
{
    public partial class AddImageToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84740d7f-2011-4f3d-a012-41a335fb0b4e"));

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Cars",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("ef27578e-6f04-4a53-a04f-7bf61f9b50fc"), "ADMIN", "ADMINOV", "123456", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ef27578e-6f04-4a53-a04f-7bf61f9b50fc"));

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("84740d7f-2011-4f3d-a012-41a335fb0b4e"), "ADMIN", "ADMINOV", "123456", "admin" });
        }
    }
}
