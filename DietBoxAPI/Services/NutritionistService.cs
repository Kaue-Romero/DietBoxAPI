using DietBoxAPI.DB;
using DietBoxAPI.DB.Models;
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

    public bool Update(int id, Nutritionist nutritionist)
    {
        var existing = _context.Nutritionists.Find(id);
        if (existing == null) return false;

        existing.Username = nutritionist.Username;
        existing.PasswordHash = nutritionist.PasswordHash; // lembre-se do hash!

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
