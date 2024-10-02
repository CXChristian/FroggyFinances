using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using expense_transactions.Models;

namespace expense_transactions.Data
{
    public static class SampleData
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure roles are created
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
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
                new ApplicationUser { Email = "aa@aa.aa", Role = "admin" },
                new ApplicationUser { Email = "mm@mm.mm", Role = "user" }
            };
        }
    }
}