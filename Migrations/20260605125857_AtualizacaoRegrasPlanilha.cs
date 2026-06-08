using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace repos.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoRegrasPlanilha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetasCobranca");

            migrationBuilder.CreateTable(
                name: "ColaboradoresMetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeColaborador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicialContrato = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFinalContrato = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MetaS1 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MetaS2 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MetaS3 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MetaS4 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RecuperadoS1 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RecuperadoS2 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RecuperadoS3 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RecuperadoS4 = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaboradoresMetas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ColaboradoresMetas",
                columns: new[] { "Id", "DataFinalContrato", "DataInicialContrato", "MetaS1", "MetaS2", "MetaS3", "MetaS4", "NomeColaborador", "RecuperadoS1", "RecuperadoS2", "RecuperadoS3", "RecuperadoS4" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 6000m, 6000m, 6000m, "BEN HUR MAIA DE ALMEIDA", 4208m, 0m, 0m, 0m },
                    { 2, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 6000m, 6000m, 6000m, "DAVI DE OLIVEIRA MELO", 3752m, 0m, 0m, 0m },
                    { 3, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 6000m, 6000m, 6000m, "GUILHERME NASCIMENTO", 7308m, 0m, 0m, 0m },
                    { 4, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 6000m, 6000m, 6000m, "GUILHERME SOARES AZEREDO", 3666m, 0m, 0m, 0m },
                    { 5, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 6000m, 6000m, 6000m, "PAMELLA PEREIRA", 1079m, 0m, 0m, 0m },
                    { 6, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 6000m, 6000m, 6000m, "VICTORIA", 2111m, 0m, 0m, 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColaboradoresMetas");

            migrationBuilder.CreateTable(
                name: "MetasCobranca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeColaborador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorAtingido = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ValorMeta = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetasCobranca", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
    }
}
