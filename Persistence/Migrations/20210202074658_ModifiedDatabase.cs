using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ModifiedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                table: "CryptoHistory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CryptoHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                table: "CryptoHistory",
                column: "CryptoForeignKey",
                principalTable: "Cryptos",
                principalColumn: "CryptoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                table: "CryptoHistory");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CryptoHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoHistory_Cryptos_CryptoForeignKey",
                table: "CryptoHistory",
                column: "CryptoForeignKey",
                principalTable: "Cryptos",
                principalColumn: "CryptoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
