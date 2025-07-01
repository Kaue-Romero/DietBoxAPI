using DietBoxAPI.DTO;

namespace DietBoxAPI.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<FoodReadDto>> GetAllAsync();
        Task<FoodReadDto?> GetByIdAsync(int id);
        Task<FoodReadDto> CreateAsync(FoodCreateDto dto);
        Task<bool> UpdateAsync(int id, FoodUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
