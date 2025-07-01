using DietBoxAPI.DB.Models;

public interface INutritionistService
{
    IEnumerable<Nutritionist> GetAll();
    Nutritionist? GetById(int id);
    Nutritionist Create(Nutritionist nutritionist);
    bool Update(int id, Nutritionist nutritionist);
    bool Delete(int id);
}
