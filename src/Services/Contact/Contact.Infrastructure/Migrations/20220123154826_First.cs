using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.Infrastructure.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "person",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    surname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    company = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_contact_info",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    info_type = table.Column<byte>(type: "smallint", nullable: false),
                    info_detail = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_contact_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_contact_info_person_person_id",
                        column: x => x.person_id,
                        principalSchema: "public",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_id",
                schema: "public",
                table: "person",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_person_contact_info_id",
                schema: "public",
                table: "person_contact_info",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_person_contact_info_info_type",
                schema: "public",
                table: "person_contact_info",
                column: "info_type");

            migrationBuilder.CreateIndex(
                name: "IX_person_contact_info_person_id",
                schema: "public",
                table: "person_contact_info",
                column: "person_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_contact_info",
                schema: "public");

            migrationBuilder.DropTable(
                name: "person",
                schema: "public");
        }
    }
}
