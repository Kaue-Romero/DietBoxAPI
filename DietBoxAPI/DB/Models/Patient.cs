using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DB.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<MealPlan> MealPlans { get; set; }
    }
}
