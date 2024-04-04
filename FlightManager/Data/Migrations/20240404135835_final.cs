using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0efa7782-607f-4353-a482-e300d6bef13b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "82f3efb2-c932-48e5-b39f-c9c64f5cb103", "7b4cdf1a-54a6-4961-9b0e-defad96b79a0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f3efb2-c932-48e5-b39f-c9c64f5cb103");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b4cdf1a-54a6-4961-9b0e-defad96b79a0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82f3efb2-c932-48e5-b39f-c9c64f5cb103", "01831fd2-a7d9-4d31-9f62-bf594ce9b423", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0efa7782-607f-4353-a482-e300d6bef13b", "1322139b-d3b1-45bc-acf2-7cd17753125c", "Employee", "employee" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SSN", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7b4cdf1a-54a6-4961-9b0e-defad96b79a0", 0, null, "ea77a4f7-49c0-417a-8c3c-e90228e0fd2d", "admin@dev.local", true, null, null, false, null, "admin@dev.local", "admin@dev.local", "AQAAAAEAACcQAAAAEPZfn0El8Sl92tgRXZJpdL5qDI14u5BwIahE6LKc3E4BSeKzNDTnBSNrIGxZxQuhdQ==", null, false, null, "b54f9b5e-0f60-422c-bf60-6189ec58b78a", false, "admin@dev.local" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "82f3efb2-c932-48e5-b39f-c9c64f5cb103", "7b4cdf1a-54a6-4961-9b0e-defad96b79a0" });
        }
    }
}
