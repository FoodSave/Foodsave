using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderStateMachine.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStateData",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CurrentState = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    OrderCreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OrderCancelDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OrderCollectedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CheckoutSessionId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PaymentMethod = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStateData", x => x.CorrelationId);
                });

            migrationBuilder.CreateTable(
                name: "PackageId",
                columns: table => new
                {
                    value = table.Column<Guid>(type: "char(36)", nullable: false),
                    OrderStateDataCorrelationId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageId", x => x.value);
                    table.ForeignKey(
                        name: "FK_PackageId_OrderStateData_OrderStateDataCorrelationId",
                        column: x => x.OrderStateDataCorrelationId,
                        principalTable: "OrderStateData",
                        principalColumn: "CorrelationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageId_OrderStateDataCorrelationId",
                table: "PackageId",
                column: "OrderStateDataCorrelationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageId");

            migrationBuilder.DropTable(
                name: "OrderStateData");
        }
    }
}
