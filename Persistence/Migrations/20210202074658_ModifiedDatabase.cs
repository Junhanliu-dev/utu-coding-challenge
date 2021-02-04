using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ModifiedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                "CryptoHistory");

            migrationBuilder.DropColumn(
                "Name",
                "CryptoHistory");

            migrationBuilder.AddForeignKey(
                "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                "CryptoHistory",
                "CryptoForeignKey",
                "Cryptos",
                principalColumn: "CryptoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                "CryptoHistory");

            migrationBuilder.AddColumn<string>(
                "Name",
                "CryptoHistory",
                "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                "CryptoHistory",
                "CryptoForeignKey",
                "Cryptos",
                principalColumn: "CryptoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}