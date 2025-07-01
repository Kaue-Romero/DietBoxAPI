namespace DietBoxAPI.Services
{
    using DietBoxAPI.DB;
    using DietBoxAPI.DB.Models;
    using DietBoxAPI.DTO;
    using DietBoxAPI.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PatientService : IPatientService
    {
        private readonly DietBoxDbContext _context;

        public PatientService(DietBoxDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientReadDto>> GetAllAsync()
        {
            var patients = await _context.Patients
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return patients.ToArray().Select(p => new PatientReadDto
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public async Task<PatientReadDto?> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return patient == null ? null : new PatientReadDto
            {
                Id = patient.Id,
                Name = patient.Name
            };
        }

        public async Task<PatientReadDto> CreateAsync(PatientCreateDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return new PatientReadDto
            {
                Id = patient.Id,
                Name = patient.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, PatientUpdateDto dto)
        {
            if (id != dto.Id) return false;

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null || patient.IsDeleted) return false;

            if (!string.IsNullOrEmpty(dto.Name))
                patient.Name = dto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null || patient.IsDeleted) return false;

            patient.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MealPlanReadDto>> AsyncGetMealPlansToday(int id)
        {

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            Console.Write(patient);
            if(patient == null)
                return Enumerable.Empty<MealPlanReadDto>();

            var today = DateOnly.FromDateTime(DateTime.Today);

            var plans = await _context.MealPlans
                .Where(mp => mp.Date == today)
                .Where(mp => mp.PatientId == id)
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

    }
}
