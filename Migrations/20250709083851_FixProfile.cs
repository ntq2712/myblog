using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class FixProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MySkills_Imange",
                table: "MyProfiles");

            migrationBuilder.RenameColumn(
                name: "MySkills_Name",
                table: "MyProfiles",
                newName: "Avatar");

            migrationBuilder.CreateTable(
                name: "MySkill",
                columns: table => new
                {
                    MyProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imange = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MySkill", x => new { x.MyProfileId, x.Id });
                    table.ForeignKey(
                        name: "FK_MySkill_MyProfiles_MyProfileId",
                        column: x => x.MyProfileId,
                        principalTable: "MyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MySkill");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "MyProfiles",
                newName: "MySkills_Name");

            migrationBuilder.AddColumn<string>(
                name: "MySkills_Imange",
                table: "MyProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
