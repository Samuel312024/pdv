using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginBanners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginBanners",
                schema: "pdv",
                columns: table => new
                {
                    BannerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagemUrl = table.Column<string>(type: "text", nullable: false),
                    ImagemCaminho = table.Column<string>(type: "text", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginBanners", x => x.BannerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginBanners",
                schema: "pdv");
        }
    }
}
