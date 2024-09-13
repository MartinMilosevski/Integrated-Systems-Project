using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedSystems.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateNameForReturns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Returns",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Returns");
        }
    }
}
