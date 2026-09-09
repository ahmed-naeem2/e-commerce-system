using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_system.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusOfCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Cart_Status",
                table: "Carts");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Cart_Status",
                table: "Carts",
                sql: "[Status] IN (N'Active',N'Cancelled',N'Completed')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Cart_Status",
                table: "Carts");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Cart_Status",
                table: "Carts",
                sql: "[Status] IN (N'Active',N'Cancelled',N'Converted',N'Abandoned')");
        }
    }
}
