using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class InitProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseStudyType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UiMetadata_Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UiMetadata_ColorLight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UiMetadata_Index = table.Column<int>(type: "int", nullable: false),
                    UiMetadata_Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GetInTouch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moblie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetInTouch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyCaseStudies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCaseStudies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MySkills_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MySkills_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MySkills_Imange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyWorkExperience",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyWorkExperience", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseStudy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MyCaseStudiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseStudy_CaseStudyType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CaseStudyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseStudy_MyCaseStudies_MyCaseStudiesId",
                        column: x => x.MyCaseStudiesId,
                        principalTable: "MyCaseStudies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudy_MyCaseStudiesId",
                table: "CaseStudy",
                column: "MyCaseStudiesId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudy_TypeId",
                table: "CaseStudy",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseStudy");

            migrationBuilder.DropTable(
                name: "GetInTouch");

            migrationBuilder.DropTable(
                name: "MyProfiles");

            migrationBuilder.DropTable(
                name: "MyWorkExperience");

            migrationBuilder.DropTable(
                name: "CaseStudyType");

            migrationBuilder.DropTable(
                name: "MyCaseStudies");
        }
    }
}
