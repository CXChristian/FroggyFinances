using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_transactions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}