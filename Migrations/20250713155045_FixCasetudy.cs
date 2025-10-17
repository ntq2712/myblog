using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class FixCasetudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseStudy_CaseStudyType_TypeId",
                table: "CaseStudy");

            migrationBuilder.DropIndex(
                name: "IX_CaseStudy_TypeId",
                table: "CaseStudy");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "CaseStudy",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CaseStudy",
                newName: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStudy_TypeId",
                table: "CaseStudy",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseStudy_CaseStudyType_TypeId",
                table: "CaseStudy",
                column: "TypeId",
                principalTable: "CaseStudyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
