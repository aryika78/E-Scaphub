using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewaste_Vs2022.Migrations
{
    public partial class Ewastethird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverMasters",
                columns: table => new
                {
                    Drvid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Daddress = table.Column<string>(type: "varchar(500)", nullable: false),
                    Ddob = table.Column<string>(type: "varchar(20)", nullable: false),
                    Dgender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Dphone = table.Column<string>(type: "varchar(15)", nullable: false),
                    Dlicimage = table.Column<string>(type: "varchar(250)", nullable: false),
                    Dimage = table.Column<string>(type: "varchar(250)", nullable: false),
                    Dvehiclenumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    DAid = table.Column<int>(type: "int", nullable: false),
                    Tid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverMasters", x => x.Drvid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverMasters");
        }
    }
}
