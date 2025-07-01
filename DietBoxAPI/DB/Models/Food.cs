using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DB.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public float Calories { get; set; }
    }
}
