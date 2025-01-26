using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stripay.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table =>
                    new
                    {
                        PaymentId = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        StripeCustomerId = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        ),
                        PaymentMethodId = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        ),
                        PaymentStatus = table.Column<int>(type: "int", nullable: false),
                        Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                        ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Payments");
        }
    }
}
