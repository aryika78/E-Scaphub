using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewaste_Vs2022.Migrations
{
    public partial class Ewastesecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaMasters",
                columns: table => new
                {
                    Aid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Areaname = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaMasters", x => x.Aid);
                });

            migrationBuilder.CreateTable(
                name: "CartMasters",
                columns: table => new
                {
                    Cartid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCid = table.Column<int>(type: "int", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    SCQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartMasters", x => x.Cartid);
                });

            migrationBuilder.CreateTable(
                name: "OrderMasters",
                columns: table => new
                {
                    Ordid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orddate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Drvid = table.Column<int>(type: "int", nullable: false),
                    Areaid = table.Column<int>(type: "int", nullable: false),
                    Ordstatus = table.Column<string>(type: "varchar(10)", nullable: false),
                    Ordtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Ordgrandtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Ordpaymentmode = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasters", x => x.Ordid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaMasters");

            migrationBuilder.DropTable(
                name: "CartMasters");

            migrationBuilder.DropTable(
                name: "OrderMasters");
        }
    }
}
