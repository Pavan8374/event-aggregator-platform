using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_Users_RoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Users_Roles_RoleId",
            //    table: "Users");

            //migrationBuilder.DropIndex(
            //    name: "IX_Users_RoleId",
            //    table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("e8311047-8829-4aa0-9d4a-39006e8e01c8"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("e8311047-8829-4aa0-9d4a-39006e8e01c8"),
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_RoleId",
            //    table: "Users",
            //    column: "RoleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Users_Roles_RoleId",
            //    table: "Users",
            //    column: "RoleId",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
