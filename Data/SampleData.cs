using Microsoft.AspNetCore.Identity;
using expense_transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_transactions.Data
{
    public static class SampleData
    {
        public static List<ApplicationUser> GetUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "aa@aa.aa", Email = "aa@aa.aa", Role = "admin", EmailConfirmed = true, IsAdminApproved = true },
                new ApplicationUser { UserName = "mm@mm.mm", Email = "mm@mm.mm", Role = "member", EmailConfirmed = true, IsAdminApproved = true }
            };
        }

        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, BucketContext bucketContext)
        {
            // Ensure roles are created
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (!await roleManager.RoleExistsAsync("member"))
            {
                await roleManager.CreateAsync(new IdentityRole("member"));
            }

            // Seed users
            var users = GetUsers();
            foreach (var user in users)
            {
                if (userManager.Users.All(u => u.Email != user.Email))
                {
                    var result = await userManager.CreateAsync(user, "P@$$w0rd");
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(user.Role))
                        {
                            await userManager.AddToRoleAsync(user, user.Role);
                        }
                    }
                }
            }

            // Seed buckets
            if (bucketContext.Buckets != null && !await bucketContext.Buckets.AnyAsync())  // Check if any buckets already exist
            {
                var buckets = GetBucket();
                bucketContext.Buckets.AddRange(buckets);
                await bucketContext.SaveChangesAsync();
            }
        }

        public static List<Bucket> GetBucket()
        {
            List<Bucket> bucket = new List<Bucket>() {
                new Bucket() {
                    Id=1,
                    Category = "Food",
                    Company = "ST JAMES RESTAURAT",
                },
                new Bucket() {
                    Id=2,
                    Category="Communication",
                    Company = "ROGERS MOBILE",
                },
                new Bucket() {
                    Id=3,
                    Category = "Groceries",
                    Company = "SAFEWAY",
                },
                new Bucket() {
                    Id=4,
                    Category="Donations",
                    Company = "RED CROSS",
                },
                new Bucket() {
                    Id=5,
                    Category="Food",
                    Company = "PUR & SIMPLE RESTAUR",
                },
                new Bucket() {
                    Id=6,
                    Category="Groceries",
                    Company = "REAL CDN SUPERS",
                },
                new Bucket() {
                    Id=7,
                    Category="Car Insurance",
                    Company = "ICBC",
                },
                new Bucket() {
                    Id=8,
                    Category="Gas Heating",
                    Company = "FORTISBC",
                },
                new Bucket() {
                    Id=9,
                    Category= "Health",
                    Company = "GATEWAY",
                },
                new Bucket() {
                    Id=10,
                    Category="Food",
                    Company = "SUBWAY",
                },
                new Bucket() {
                    Id=11,
                    Category="Government",
                    Company = "GC",
                },
                new Bucket() {
                    Id=12,
                    Category="Banking",
                    Company = "CHQ",
                },
                new Bucket() {
                    Id=13,
                    Category="Banking",
                    Company = "BMO",
                },
                new Bucket() {
                    Id=14,
                    Category="Groceries",
                    Company = "WALMART",
                },
                new Bucket() {
                    Id=15,
                    Category="Food",
                    Company = "MCDONALDS",
                },
                new Bucket() {
                    Id=16,
                    Category="Food",
                    Company = "WHITE SPOT RESTAURAN",
                },
                new Bucket() {
                    Id=17,
                    Category="Communication",
                    Company = "Shaw Cable",
                },
                new Bucket() {
                    Id=18,
                    Category="Retail",
                    Company = "CANADIAN TIRE",
                },
                new Bucket() {
                    Id=19,
                    Category= "Donations",
                    Company = "WORLD VISION",
                },
                new Bucket() {
                    Id=20,
                    Category= "Food",
                    Company = "TIM HORTONS",
                },
                new Bucket() {
                    Id=21,
                    Category= "Food",
                    Company = "7-ELEVEN STORE",
                },
                new Bucket() {
                    Id=22,
                    Category= "Banking",
                    Company = "O.D.P FEE",
                },
                new Bucket() {
                    Id=23,
                    Category= "Banking",
                    Company = "MONTHLY ACCOUNT FEE",
                },
            };
            return bucket;
        }
    }
}