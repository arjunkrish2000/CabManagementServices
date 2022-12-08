using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabManagementSystem__Backup_4.Migrations
{
    public partial class DriverDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenceNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CabName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverDetails_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverDetails_DriverId",
                table: "DriverDetails",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverDetails_LicenceNo",
                table: "DriverDetails",
                column: "LicenceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverDetails_RegistrationNo",
                table: "DriverDetails",
                column: "RegistrationNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverDetails");
        }
    }
}
