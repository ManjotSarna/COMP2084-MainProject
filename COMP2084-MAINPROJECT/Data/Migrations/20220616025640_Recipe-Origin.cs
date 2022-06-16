using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2084_MAINPROJECT.Data.Migrations
{
    public partial class RecipeOrigin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginId",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Origin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_OriginId",
                table: "Recipe",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Origin_OriginId",
                table: "Recipe",
                column: "OriginId",
                principalTable: "Origin",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Origin_OriginId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "Origin");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_OriginId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "Recipe");
        }
    }
}
