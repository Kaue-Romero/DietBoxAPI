using DietBoxAPI.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DietBoxAPI.DB
{
    public class DietBoxDbContext : DbContext
    {
        public DietBoxDbContext(DbContextOptions<DietBoxDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Nutritionist> Nutritionists { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanFood> MealPlanFoods { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar a chave primária composta para MealPlanFood (many-to-many)
            modelBuilder.Entity<MealPlanFood>()
                .HasKey(mp => new { mp.MealPlanId, mp.FoodId });

            // Configurar relacionamento MealPlanFood
            modelBuilder.Entity<MealPlanFood>()
                .HasOne(mp => mp.MealPlan)
                .WithMany(m => m.MealPlanFoods)
                .HasForeignKey(mp => mp.MealPlanId);

            modelBuilder.Entity<MealPlanFood>()
                .HasOne(mp => mp.Food)
                .WithMany()
                .HasForeignKey(mp => mp.FoodId);

            // Relação Patient -> MealPlans (1:N)
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.MealPlans)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
