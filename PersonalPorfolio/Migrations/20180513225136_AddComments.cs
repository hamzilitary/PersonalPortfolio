using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalPortfolio.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentKey = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AssociatedBlogBlogKey = table.Column<int>(nullable: true),
                    CommentText = table.Column<string>(nullable: true),
                    ReplyDtm = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentKey);
                    table.ForeignKey(
                        name: "FK_Comments_Blogs_AssociatedBlogBlogKey",
                        column: x => x.AssociatedBlogBlogKey,
                        principalTable: "Blogs",
                        principalColumn: "BlogKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AssociatedBlogBlogKey",
                table: "Comments",
                column: "AssociatedBlogBlogKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
