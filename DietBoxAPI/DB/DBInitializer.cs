using DietBoxAPI.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DietBoxAPI.DB
{
    public static class DBInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DietBoxDbContext>();

            context.Database.Migrate();

            // Seed de Admin
            if (!context.Admins.Any())
            {
                context.Admins.Add(new Admin
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
                });
            }

            // Seed de Nutritionist
            if (!context.Nutritionists.Any())
            {
                context.Nutritionists.Add(new Nutritionist
                {
                    Username = "nutri",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("nutri123")
                });
            }

            context.SaveChanges();
        }
    }
}
