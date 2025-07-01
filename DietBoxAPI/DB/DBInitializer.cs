using Bogus;
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

            if (!context.Nutritionists.Any())
            {
                context.Nutritionists.Add(new Nutritionist
                {
                    Username = "nutri",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("nutri123")
                });
            }

            if (!context.Foods.Any())
            {
                var foodFaker = new Faker<Food>()
                    .RuleFor(f => f.Name, f => f.Commerce.ProductName())
                    .RuleFor(f => f.Calories, f => f.Random.Int(50, 1500));

                var foods = foodFaker.Generate(50);
                context.Foods.AddRange(foods);
            }

            if (!context.Patients.Any())
            {
                var patientFaker = new Faker<Patient>()
                    .RuleFor(p => p.Name, f => f.Name.FullName());

                var patients = patientFaker.Generate(30);
                context.Patients.AddRange(patients);
            }

            context.SaveChanges();
        }
    }
}
