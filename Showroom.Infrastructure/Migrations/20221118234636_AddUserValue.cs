using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showroom.Infrastructure.Migrations
{
    public partial class AddUserValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("c910087a-0c28-43dd-b8b9-a9470f081c93"), "ADMIN", "ADMINOV", "123456", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c910087a-0c28-43dd-b8b9-a9470f081c93"));
        }
    }
}
