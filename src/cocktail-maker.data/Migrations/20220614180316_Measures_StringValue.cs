using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailMaker.Data.Migrations
{
    public partial class Measures_StringValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unit",
                table: "Measures");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "Measures",
                type: "text",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "value",
                table: "Measures",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "Measures",
                type: "text",
                nullable: true);
        }
    }
}
