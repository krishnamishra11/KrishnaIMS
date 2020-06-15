using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_PurchaseOrders_PurchaseOrderId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderId",
                table: "Item",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_PurchaseOrders_PurchaseOrderId",
                table: "Item",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_PurchaseOrders_PurchaseOrderId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_PurchaseOrders_PurchaseOrderId",
                table: "Item",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
