using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedVirtualProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemId1",
                table: "Wishlists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Wishlists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId1",
                table: "PurchaseHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "PurchaseHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ItemId",
                table: "Wishlists",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ItemId1",
                table: "Wishlists",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId1",
                table: "Wishlists",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_ItemId",
                table: "PurchaseHistories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_ItemId1",
                table: "PurchaseHistories",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_UserId",
                table: "PurchaseHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_UserId1",
                table: "PurchaseHistories",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Users_UserId",
                table: "Logins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Items_ItemId",
                table: "PurchaseHistories",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Items_ItemId1",
                table: "PurchaseHistories",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Users_UserId",
                table: "PurchaseHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Users_UserId1",
                table: "PurchaseHistories",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Items_ItemId",
                table: "Wishlists",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Items_ItemId1",
                table: "Wishlists",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId1",
                table: "Wishlists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_UserId",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Items_ItemId",
                table: "PurchaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Items_ItemId1",
                table: "PurchaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Users_UserId",
                table: "PurchaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Users_UserId1",
                table: "PurchaseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Items_ItemId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Items_ItemId1",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId1",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ItemId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ItemId1",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId1",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_ItemId",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_ItemId1",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_UserId",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_UserId1",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_Logins_UserId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PurchaseHistories");
        }
    }
}
