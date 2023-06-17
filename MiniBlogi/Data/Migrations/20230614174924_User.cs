using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlogi.Data.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_BlogPosts_BlogPostId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_BlogPostId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "BlogPostImage",
                columns: table => new
                {
                    BlogPostsId = table.Column<int>(type: "int", nullable: false),
                    ImagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostImage", x => new { x.BlogPostsId, x.ImagesId });
                    table.ForeignKey(
                        name: "FK_BlogPostImage_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostImage_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostImage_ImagesId",
                table: "BlogPostImage",
                column: "ImagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostImage");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_BlogPostId",
                table: "Images",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_BlogPosts_BlogPostId",
                table: "Images",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
