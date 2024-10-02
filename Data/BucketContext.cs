using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_transactions.Data;
using expense_transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_transactions.Data
{
    public class BucketContext : DbContext 
    {
        public BucketContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Bucket>().ToTable("Buckets");
            builder.Entity<Bucket>().HasData(SampleData.GetBucket());
        }
        public DbSet<Bucket>? Buckets { get; set; }
    }
}