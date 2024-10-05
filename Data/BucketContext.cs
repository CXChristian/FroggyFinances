using expense_transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_transactions.Data
{
    public class BucketContext : DbContext
    {
        public BucketContext(DbContextOptions<BucketContext> options) : base(options) { }
        public DbSet<Bucket>? Buckets { get; set; }
    }
}