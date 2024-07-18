using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_website.Migrations
{
    /// <inheritdoc />
    public partial class RenameAdminToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Admins_AdminId",
                table: "Scripts");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Scripts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Scripts_AdminId",
                table: "Scripts",
                newName: "IX_Scripts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Admins_UserId",
                table: "Scripts",
                column: "UserId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Admins_UserId",
                table: "Scripts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Scripts",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Scripts_UserId",
                table: "Scripts",
                newName: "IX_Scripts_AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Admins_AdminId",
                table: "Scripts",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
