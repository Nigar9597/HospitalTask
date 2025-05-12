using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalTask.Migrations
{
    /// <inheritdoc />
    public partial class CreateDoctor3sTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor3s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department3Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor3s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor3s_Department3s_Department3Id",
                        column: x => x.Department3Id,
                        principalTable: "Department3s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor3s_Department3Id",
                table: "Doctor3s",
                column: "Department3Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor3s");
        }
    }
}
