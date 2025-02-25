using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAndPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SectionOder",
                table: "PostSection",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Heading",
                table: "PostSection",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "PostSection",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ViewCount",
                table: "Post",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthDay",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "PostSection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Post",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TotalComments",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLikes",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalViews",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "PostSection");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "TotalComments",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "TotalLikes",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "TotalViews",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "PostSection",
                newName: "SectionOder");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "PostSection",
                newName: "Heading");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "PostSection",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Post",
                newName: "ViewCount");
        }
    }
}
