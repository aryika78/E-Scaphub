using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewaste_Vs2022.Migrations
{
    public partial class Ewastefirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplainMasters",
                columns: table => new
                {
                    Cid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Cdetails = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainMasters", x => x.Cid);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackMasters",
                columns: table => new
                {
                    Fid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Feedbackdesc = table.Column<string>(type: "varchar(500)", nullable: false),
                    Feedbackdate = table.Column<string>(type: "varchar(50)", nullable: false),
                    ExperienceRate = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackMasters", x => x.Fid);
                });

            migrationBuilder.CreateTable(
                name: "PersonMasters",
                columns: table => new
                {
                    Pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Paddress = table.Column<string>(type: "varchar(500)", nullable: false),
                    Pdob = table.Column<string>(type: "varchar(20)", nullable: false),
                    Pgender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Pphone = table.Column<string>(type: "varchar(15)", nullable: false),
                    Pemail = table.Column<string>(type: "varchar(50)", nullable: false),
                    Ppassword = table.Column<string>(type: "varchar(50)", nullable: false),
                    Pimage = table.Column<string>(type: "varchar(250)", nullable: false),
                    Pqid = table.Column<int>(type: "int", nullable: false),
                    Panswer = table.Column<string>(type: "varchar(150)", nullable: false),
                    Proleid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMasters", x => x.Pid);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategoryMasters",
                columns: table => new
                {
                    Catid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Catname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Catimage = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategoryMasters", x => x.Catid);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubCategories",
                columns: table => new
                {
                    SCid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCname = table.Column<string>(type: "varchar(100)", nullable: false),
                    SCimage = table.Column<string>(type: "varchar(250)", nullable: false),
                    SCpriceperunit = table.Column<int>(type: "int", nullable: false),
                    Catid = table.Column<int>(type: "int", nullable: false),
                    SCdesc = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategories", x => x.SCid);
                });

            migrationBuilder.CreateTable(
                name: "QuestionMasters",
                columns: table => new
                {
                    Qid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMasters", x => x.Qid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplainMasters");

            migrationBuilder.DropTable(
                name: "FeedbackMasters");

            migrationBuilder.DropTable(
                name: "PersonMasters");

            migrationBuilder.DropTable(
                name: "ProductCategoryMasters");

            migrationBuilder.DropTable(
                name: "ProductSubCategories");

            migrationBuilder.DropTable(
                name: "QuestionMasters");
        }
    }
}
