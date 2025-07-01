using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DTO
{
    public class MealPlanCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int PatientId { get; set; }

        public List<int> FoodIds { get; set; } = new();
    }

    public class MealPlanReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int PatientId { get; set; }

        public float TotalCalories { get; set; }

        public List<FoodReadDto> Foods { get; set; } = new();
    }

}
