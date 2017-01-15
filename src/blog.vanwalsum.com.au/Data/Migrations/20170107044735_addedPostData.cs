using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.vanwalsum.com.au.Data.Migrations
{
    public partial class addedPostData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeaderImageUrl",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryImageUrl",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SummaryImageUrl",
                table: "Posts");
        }
    }
}
