using Microsoft.AspNetCore.Identity;
using expense_transactions.Models;

namespace expense_transactions.Data
{
    public static class SampleData
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
                        await userManager.AddToRoleAsync(user, user.Role);
                    }
                }
            }
        }

        public static List<ApplicationUser> GetUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "aa@aa.aa", Email = "aa@aa.aa", Role = "admin", EmailConfirmed = true },
                new ApplicationUser { UserName = "mm@mm.mm", Email = "mm@mm.mm", Role = "member", EmailConfirmed = true }
            };
        }

        public static List<Bucket> GetBucket()
        {
            List<Bucket> bucket = new List<Bucket>() {
                new Bucket() {
                    Id=1,
                    Category = "Entertainment",
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
                    Category="Entertainment",
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
            };
            return bucket;
        }
    }
}