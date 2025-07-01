using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DB.Models
{
    public class MealPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Relação muitos para muitos com Food
        public ICollection<MealPlanFood> MealPlanFoods { get; set; }
    }
}
