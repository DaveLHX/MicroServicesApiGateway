using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonListsApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CommonListsApi");

            migrationBuilder.CreateTable(
                name: "Element",
                schema: "CommonListsApi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<byte>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Element", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrmsPers",
                schema: "CommonListsApi",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    RankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmsPers", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                schema: "CommonListsApi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Element = table.Column<int>(nullable: false),
                    Sequence = table.Column<byte>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Element",
                schema: "CommonListsApi");

            migrationBuilder.DropTable(
                name: "HrmsPers",
                schema: "CommonListsApi");

            migrationBuilder.DropTable(
                name: "Rank",
                schema: "CommonListsApi");
        }
    }
}
