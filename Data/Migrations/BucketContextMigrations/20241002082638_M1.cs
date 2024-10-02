using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace expense_transactions.Data.Migrations.BucketContextMigrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buckets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Company = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buckets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Buckets",
                columns: new[] { "Id", "Category", "Company" },
                values: new object[,]
                {
                    { 1, "Entertainment", "ST JAMES RESTAURAT" },
                    { 2, "Communication", "ROGERS MOBILE" },
                    { 3, "Groceries", "SAFEWAY" },
                    { 4, "Donations", "RED CROSS" },
                    { 5, "Entertainment", "PUR & SIMPLE RESTAUR" },
                    { 6, "Groceries", "REAL CDN SUPERS" },
                    { 7, "Car Insurance", "ICBC" },
                    { 8, "Gas Heating", "FORTISBC" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buckets");
        }
    }
}
