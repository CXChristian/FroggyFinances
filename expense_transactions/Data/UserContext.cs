using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_transactions.Data;
using expense_transactions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasData(SampleData.GetUsers());
    }


    public DbSet<User> Users { get; set; }
}