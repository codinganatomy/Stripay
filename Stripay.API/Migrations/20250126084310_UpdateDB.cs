using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stripay.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardExpiryMonth",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "CardExpiryYear",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<string>(
                name: "CardLast4Digits",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "CardName",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "ChargeId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CardExpiryMonth", table: "Payments");

            migrationBuilder.DropColumn(name: "CardExpiryYear", table: "Payments");

            migrationBuilder.DropColumn(name: "CardLast4Digits", table: "Payments");

            migrationBuilder.DropColumn(name: "CardName", table: "Payments");

            migrationBuilder.DropColumn(name: "ChargeId", table: "Payments");
        }
    }
}
