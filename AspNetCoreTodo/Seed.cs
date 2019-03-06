using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public static class SeedData
    {
        public static async Task InitalizeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> RoleManager)
        {
            var roleExists = await RoleManager.RoleExistsAsync(Constants.AdministratorRole);
            if(roleExists) return;

            await RoleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> UserManager)
        {
            var testAdmin = await UserManager.Users                
                .SingleOrDefaultAsync(u => u.Email == "admin@todo.local");

            if(testAdmin != null) return;

            testAdmin = new IdentityUser 
            {
                UserName = "admin@todo.local",
                Email    = "admin@todo.local"
            };

            await UserManager.CreateAsync(testAdmin, "NotSecure123!!");
            await UserManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    }

    public static class Constants 
    {
        public const string AdministratorRole = "Administrator";
    }
}