using DietBoxAPI.DTO;

namespace DietBoxAPI.Interfaces
{
    public interface IMealPlanService
    {
        Task<IEnumerable<MealPlanReadDto>> GetAllAsync();
        Task<MealPlanReadDto?> GetByIdAsync(int id);
        Task<MealPlanReadDto> CreateAsync(MealPlanCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
