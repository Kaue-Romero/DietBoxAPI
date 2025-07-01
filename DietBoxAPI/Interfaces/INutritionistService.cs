using DietBoxAPI.DB.Models;
using DietBoxAPI.DTO;

public interface INutritionistService
{
    IEnumerable<Nutritionist> GetAll();
    Nutritionist? GetById(int id);
    Nutritionist Create(Nutritionist nutritionist);
    bool Update(int id, NutritionistUpdateDto nutritionist);
    bool Delete(int id);
}
