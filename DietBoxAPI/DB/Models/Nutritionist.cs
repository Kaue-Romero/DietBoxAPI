using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DietBoxAPI.DB.Models
{
    public class Nutritionist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string Role => "Nutritionist";
    }
}
