using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace repos.Migrations
{
    /// <inheritdoc />
    public partial class SeedColaboradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MetasCobranca",
                columns: new[] { "Id", "NomeColaborador", "ValorAtingido", "ValorMeta" },
                values: new object[,]
                {
                    { 1, "Guilherme", 0m, 10000m },
                    { 2, "Davi", 0m, 10000m },
                    { 3, "Ben", 0m, 10000m },
                    { 4, "Pamela", 0m, 10000m },
                    { 5, "Vitoria", 0m, 10000m },
                    { 6, "Ana", 0m, 10000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MetasCobranca",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MetasCobranca",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MetasCobranca",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MetasCobranca",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MetasCobranca",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MetasCobranca",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
