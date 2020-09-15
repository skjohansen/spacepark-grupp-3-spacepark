using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePort.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Parkinglot",
                columns: table => new
                {
                    ParkinglotId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkinglot", x => x.ParkinglotId);
                });

            migrationBuilder.CreateTable(
                name: "Parkingspot",
                columns: table => new
                {
                    ParkingspotId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(nullable: false),
                    Occupied = table.Column<bool>(nullable: false),
                    ParkinglotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkingspot", x => x.ParkingspotId);
                    table.ForeignKey(
                        name: "FK_Parkingspot_Parkinglot_ParkinglotId",
                        column: x => x.ParkinglotId,
                        principalTable: "Parkinglot",
                        principalColumn: "ParkinglotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(nullable: false),
                    RegistrationTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ParkingspotId = table.Column<int>(nullable: true),
                    DriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_Receipt_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_Parkingspot_ParkingspotId",
                        column: x => x.ParkingspotId,
                        principalTable: "Parkingspot",
                        principalColumn: "ParkingspotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Parkinglot",
                columns: new[] { "ParkinglotId", "Name" },
                values: new object[] { 1, "Hoth" });

            migrationBuilder.InsertData(
                table: "Parkinglot",
                columns: new[] { "ParkinglotId", "Name" },
                values: new object[] { 2, "Kamino" });

            migrationBuilder.InsertData(
                table: "Parkinglot",
                columns: new[] { "ParkinglotId", "Name" },
                values: new object[] { 3, "Dagobah" });

            migrationBuilder.InsertData(
                table: "Parkingspot",
                columns: new[] { "ParkingspotId", "Occupied", "ParkinglotId", "Size" },
                values: new object[,]
                {
                    { 1, false, 1, 1 },
                    { 2, false, 1, 2 },
                    { 3, false, 1, 3 },
                    { 4, false, 2, 1 },
                    { 5, false, 2, 2 },
                    { 6, false, 2, 3 },
                    { 7, false, 3, 1 },
                    { 8, false, 3, 2 },
                    { 9, false, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parkingspot_ParkinglotId",
                table: "Parkingspot",
                column: "ParkinglotId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_DriverId",
                table: "Receipt",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_ParkingspotId",
                table: "Receipt",
                column: "ParkingspotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Parkingspot");

            migrationBuilder.DropTable(
                name: "Parkinglot");
        }
    }
}
