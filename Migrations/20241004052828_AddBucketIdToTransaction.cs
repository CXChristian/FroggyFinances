using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expense_transactions.Migrations
{
    /// <inheritdoc />
    public partial class AddBucketIdToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BucketId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bucket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Company = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bucket", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BucketId",
                table: "Transactions",
                column: "BucketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bucket_BucketId",
                table: "Transactions",
                column: "BucketId",
                principalTable: "Bucket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bucket_BucketId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Bucket");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BucketId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BucketId",
                table: "Transactions");
        }
    }
}
