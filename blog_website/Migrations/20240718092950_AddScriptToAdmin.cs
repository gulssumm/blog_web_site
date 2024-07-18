using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_website.Migrations
{
    /// <inheritdoc />
    public partial class AddScriptToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Scripts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_AdminId",
                table: "Scripts",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Admins_AdminId",
                table: "Scripts",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Admins_AdminId",
                table: "Scripts");

            migrationBuilder.DropIndex(
                name: "IX_Scripts_AdminId",
                table: "Scripts");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Scripts");
        }
    }
}
