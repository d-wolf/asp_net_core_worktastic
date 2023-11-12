using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace worktastic.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedOwnerUserNameToJobPostingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserName",
                table: "JobPostings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUserName",
                table: "JobPostings");
        }
    }
}
