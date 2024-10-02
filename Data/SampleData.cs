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

            if (!await roleManager.RoleExistsAsync("user"))
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
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
                new ApplicationUser { UserName = "mm@mm.mm", Email = "mm@mm.mm", Role = "user", EmailConfirmed = true }
            };
        }
    }
}