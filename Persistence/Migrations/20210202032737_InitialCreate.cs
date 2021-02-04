using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Cryptos",
                table => new
                {
                    CryptoId = table.Column<Guid>("uuid", nullable: false),
                    CryptoName = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Cryptos", x => x.CryptoId); });

            migrationBuilder.CreateTable(
                "CryptoHistory",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CryptoForeignKey = table.Column<Guid>("uuid", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Date = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Open = table.Column<double>("double precision", nullable: false),
                    High = table.Column<double>("double precision", nullable: false),
                    Low = table.Column<double>("double precision", nullable: false),
                    Close = table.Column<double>("double precision", nullable: false),
                    Volume = table.Column<long>("bigint", nullable: false),
                    MarketCap = table.Column<long>("bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoHistory", x => x.Id);
                    table.ForeignKey(
                        "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                        x => x.CryptoForeignKey,
                        "Cryptos",
                        "CryptoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_CryptoHistory_CryptoForeignKey",
                "CryptoHistory",
                "CryptoForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CryptoHistory");

            migrationBuilder.DropTable(
                "Cryptos");
        }
    }
}