using DietBoxAPI.DB;
using DietBoxAPI.DB.Models;
using DietBoxAPI.DTO;
using Microsoft.EntityFrameworkCore;

public class NutritionistService : INutritionistService
{
    private readonly DietBoxDbContext _context;

    public NutritionistService(DietBoxDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Nutritionist> GetAll() => _context.Nutritionists.AsNoTracking().ToList();

    public Nutritionist? GetById(int id) => _context.Nutritionists.Find(id);

    public Nutritionist Create(Nutritionist nutritionist)
    {
        _context.Nutritionists.Add(nutritionist);
        _context.SaveChanges();
        return nutritionist;
    }

    public bool Update(int id, NutritionistUpdateDto dto)
    {
        var existing = _context.Nutritionists.Find(id);
        if (existing == null) return false;

        // Atualiza só se o campo não for nulo ou vazio
        if (!string.IsNullOrEmpty(dto.Username))
        {
            existing.Username = dto.Username;
        }

        if (!string.IsNullOrEmpty(dto.PasswordHash))
        {
            existing.PasswordHash = dto.PasswordHash; // lembre-se de hashear antes, se aplicável
        }

        // atualizar outros campos conforme necessário...

        _context.SaveChanges();
        return true;
    }


    public bool Delete(int id)
    {
        var nutritionist = _context.Nutritionists.Find(id);
        if (nutritionist == null) return false;

        _context.Nutritionists.Remove(nutritionist);
        _context.SaveChanges();
        return true;
    }
}
