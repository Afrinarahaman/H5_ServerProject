using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H5_ServerProject.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "ToDos",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "ToDos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title", "User_Id" },
                values: new object[] { "Description1", "Title1", new Guid("35e39329-3a7a-4e35-9978-90bedc762867") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDos",
                newName: "id");

            migrationBuilder.UpdateData(
                table: "ToDos",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Description", "Title", "User_Id" },
                values: new object[] { "Description", "Title", new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
