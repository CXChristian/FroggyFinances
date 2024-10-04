using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expense_transactions.Migrations
{
    /// <inheritdoc />
    public partial class ConsolidateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bucket_BucketId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bucket",
                table: "Bucket");

            migrationBuilder.RenameTable(
                name: "Bucket",
                newName: "Buckets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buckets",
                table: "Buckets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Buckets_BucketId",
                table: "Transactions",
                column: "BucketId",
                principalTable: "Buckets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Buckets_BucketId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buckets",
                table: "Buckets");

            migrationBuilder.RenameTable(
                name: "Buckets",
                newName: "Bucket");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bucket",
                table: "Bucket",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bucket_BucketId",
                table: "Transactions",
                column: "BucketId",
                principalTable: "Bucket",
                principalColumn: "Id");
        }
    }
}
