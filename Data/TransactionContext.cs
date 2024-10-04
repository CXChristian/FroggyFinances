using System;
using expense_transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_transactions.Data;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
    {
    }

    public DbSet<TransactionModel> Transactions { get; set; }
}
