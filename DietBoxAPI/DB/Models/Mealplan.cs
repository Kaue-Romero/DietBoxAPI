using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DB.Models
{
    public class MealPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateOnly Date { get; set; }

        // Relação muitos para muitos com Food
        public ICollection<MealPlanFood> MealPlanFoods { get; set; }
    }
}
