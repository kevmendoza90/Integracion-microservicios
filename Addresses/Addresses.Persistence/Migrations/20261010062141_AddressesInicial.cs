using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Addresses.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddressesInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses_Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses_State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    InternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_State_Addresses_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Addresses_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses_City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    InternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_City_Addresses_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Addresses_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_City_Addresses_State_StateId",
                        column: x => x.StateId,
                        principalTable: "Addresses_State",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses_Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    InternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Position_Addresses_City_CityId",
                        column: x => x.CityId,
                        principalTable: "Addresses_City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_City_CountryId",
                table: "Addresses_City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_City_StateId",
                table: "Addresses_City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Position_CityId",
                table: "Addresses_Position",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_State_CountryId",
                table: "Addresses_State",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses_Position");

            migrationBuilder.DropTable(
                name: "Addresses_City");

            migrationBuilder.DropTable(
                name: "Addresses_State");

            migrationBuilder.DropTable(
                name: "Addresses_Country");
        }
    }
}
