using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partytime.Joined.Service.Migrations
{
    /// <inheritdoc />
    public partial class RelationalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "joined",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    partyid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: true),
                    joineddate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    accepteddate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    accepted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_joined", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "joined");
        }
    }
}
