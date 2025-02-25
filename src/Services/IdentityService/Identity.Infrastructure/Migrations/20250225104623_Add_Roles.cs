using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("e8311047-8829-4aa0-9d4a-39006e8e01c8"));

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("3c94539e-8c14-4e98-9241-64ba31a6da54"), "Users who can moderate content and discussions", "Moderator", 7 },
                    { new Guid("64f55c17-c5d1-4c1e-8cbb-1a48f6c3c1d5"), "Platform administrators with full system access", "Admin", 5 },
                    { new Guid("a8a05f76-9c4f-4dd9-a8fd-dd9d927aad95"), "Internal system services for automation", "System Service", 6 },
                    { new Guid("b7f76f67-2b1b-4d4e-a13c-95d5022bc714"), "Users who can create and manage advertisements", "Advertiser", 8 },
                    { new Guid("bd2f7c20-d56c-409a-a60b-7c178e0130c5"), "Special guests who present or perform at events", "Speaker/Performer", 4 },
                    { new Guid("c3a1a8c4-524f-4fd5-8ebd-7d87790347d3"), "Partner businesses that can offer services", "Vendor", 3 },
                    { new Guid("e8311047-8829-4aa0-9d4a-39006e8e01c8"), "Regular users who attend events", "Attendee", 1 },
                    { new Guid("ee8611ad-bb96-4a42-97a1-2bd99763abe5"), "Users who can create and manage events", "Organizer", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedAt",
                table: "Users",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastLoginAt",
                table: "Users",
                column: "LastLoginAt");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatedAt",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IsActive",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LastLoginAt",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
