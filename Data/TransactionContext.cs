using System;
using expense_transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_transactions.Data;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

    public DbSet<TransactionModel> Transactions { get; set; }
    public DbSet<Bucket> Buckets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set table names and relationships if needed
        modelBuilder.Entity<TransactionModel>().ToTable("Transactions");
        modelBuilder.Entity<Bucket>().ToTable("Buckets");

        // Configure foreign key relationship between Transactions and Buckets
        modelBuilder.Entity<TransactionModel>()
            .HasOne(t => t.Bucket)
            .WithMany(b => b.Transactions)
            .HasForeignKey(t => t.BucketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}