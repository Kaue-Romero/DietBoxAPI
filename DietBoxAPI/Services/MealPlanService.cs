using DietBoxAPI.DB;
using DietBoxAPI.DB.Models;
using DietBoxAPI.DTO;
using DietBoxAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DietBoxAPI.Services
{
    public class MealPlanService : IMealPlanService
    {
        private readonly DietBoxDbContext _context;

        public MealPlanService(DietBoxDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MealPlanReadDto>> GetAllAsync()
        {
            var plans = await _context.MealPlans
                .Include(mp => mp.MealPlanFoods)
                    .ThenInclude(mpf => mpf.Food)
                .ToListAsync();

            return plans.Select(plan =>
            {
                var foods = plan.MealPlanFoods.Select(mpf => new FoodReadDto
                {
                    Id = mpf.Food.Id,
                    Name = mpf.Food.Name,
                    Calories = mpf.Food.Calories
                }).ToList();

                return new MealPlanReadDto
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Date = plan.Date,
                    PatientId = plan.PatientId,
                    Foods = foods,
                    TotalCalories = foods.Sum(f => f.Calories)
                };
            });
        }

        public async Task<MealPlanReadDto?> GetByIdAsync(int id)
        {
            var plan = await _context.MealPlans
                .Include(mp => mp.MealPlanFoods)
                    .ThenInclude(mpf => mpf.Food)
                .FirstOrDefaultAsync(mp => mp.Id == id);

            if (plan == null) return null;

            return new MealPlanReadDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Date = plan.Date,
                PatientId = plan.PatientId,
                Foods = plan.MealPlanFoods.Select(mpf => new FoodReadDto
                {
                    Id = mpf.Food.Id,
                    Name = mpf.Food.Name,
                    Calories = mpf.Food.Calories
                }).ToList()
            };
        }

        public async Task<MealPlanReadDto> CreateAsync(MealPlanCreateDto dto)
        {
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null)
                throw new Exception("Paciente não encontrado.");

            var mealPlan = new MealPlan
            {
                Name = dto.Name,
                Date = dto.Date,
                PatientId = dto.PatientId,
                MealPlanFoods = dto.FoodIds.Select(fid => new MealPlanFood
                {
                    FoodId = fid
                }).ToList()
            };

            _context.MealPlans.Add(mealPlan);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(mealPlan.Id)
                ?? throw new Exception("Erro ao retornar o plano recém-criado.");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await _context.MealPlans.FindAsync(id);
            if (plan == null) return false;

            _context.MealPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
