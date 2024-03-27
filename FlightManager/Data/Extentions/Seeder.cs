using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extentions
{
    internal static class Seeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            const string ADMIN_ROLE_ID = "82f3efb2-c932-48e5-b39f-c9c64f5cb103";
            const string EMPLOYEE_ROLE_ID = "0efa7782-607f-4353-a482-e300d6bef13b";
            const string ADMIN_ID = "7b4cdf1a-54a6-4961-9b0e-defad96b79a0";

            // Add admin role
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "Admin".ToLower()
                },
                new IdentityRole
                {
                    Id = EMPLOYEE_ROLE_ID,
                    Name = "Employee",
                    NormalizedName = "Employee".ToLower()
                }
            );

            // Add admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = ADMIN_ID,
                    UserName = "admin@dev.local",
                    NormalizedUserName = "admin@dev.local",
                    Email = "admin@dev.local",
                    NormalizedEmail = "admin@dev.local",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "password")
                }
            );

            // Assign admin user to admin role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
