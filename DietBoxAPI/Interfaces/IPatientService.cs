using DietBoxAPI.DB.Models;
using DietBoxAPI.DTO;

public interface IPatientService
{
    Task<IEnumerable<PatientReadDto>> GetAllAsync();
    Task<PatientReadDto?> GetByIdAsync(int id);
    Task<PatientReadDto> CreateAsync(PatientCreateDto dto);
    Task<bool> UpdateAsync(int id, PatientUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<MealPlanReadDto>> AsyncGetMealPlansToday(int id);
}

