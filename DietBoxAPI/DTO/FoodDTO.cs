using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DTO
{
    public class FoodCreateDto
    {
        [Required]
        public string Name { get; set; }

        public float Calories { get; set; }
    }

    public class FoodUpdateDto
    {
        public string? Name { get; set; }

        public float? Calories { get; set; }
    }

    public class FoodReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
    }

}
