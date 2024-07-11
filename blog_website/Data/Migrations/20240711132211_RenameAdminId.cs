using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_website.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameAdminId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Admins",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Admins",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Admins",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Admins",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Admins",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Admins",
                newName: "id");
        }
    }
}
