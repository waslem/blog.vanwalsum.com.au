using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.vanwalsum.com.au.Data.Migrations
{
    public partial class AddedToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contents",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contents",
                table: "Posts");
        }
    }
}
