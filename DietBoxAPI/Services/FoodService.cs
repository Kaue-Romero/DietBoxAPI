using DietBoxAPI.DB;
using DietBoxAPI.DB.Models;
using DietBoxAPI.DTO;
using DietBoxAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

public class FoodService : IFoodService
{
    private readonly DietBoxDbContext _context;

    public FoodService(DietBoxDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FoodReadDto>> GetAllAsync()
    {
        var foods = await _context.Foods.ToListAsync();

        return foods.ToArray().Select(f => new FoodReadDto
        {
            Id = f.Id,
            Name = f.Name,
            Calories = f.Calories
        });
    }

    public async Task<FoodReadDto?> GetByIdAsync(int id)
    {
        var food = await _context.Foods
            .FirstOrDefaultAsync();

        if (food == null) return null;

        return new FoodReadDto
        {
            Id = food.Id,
            Name = food.Name,
            Calories = food.Calories
        };
    }

    public async Task<FoodReadDto> CreateAsync(FoodCreateDto dto)
    {
        var food = new Food
        {
            Name = dto.Name,
            Calories = dto.Calories
        };

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();

        return new FoodReadDto
        {
            Id = food.Id,
            Name = food.Name,
            Calories = food.Calories
        };
    }

    public async Task<bool> UpdateAsync(int id, FoodUpdateDto dto)
    {

        var food = await _context.Foods.FindAsync(id);
        if (food == null) return false;

        if (!string.IsNullOrEmpty(dto.Name))
            food.Name = dto.Name;

        if (dto.Calories.HasValue)
            food.Calories = dto.Calories.Value;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        //remove the food from the database
        var food = await _context.Foods.FindAsync(id);
        if (food == null) return false;
        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();
        return true;
    }
}
