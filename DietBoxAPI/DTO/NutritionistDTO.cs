using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DTO
{
    public class NutritionistCreateDto
    {
        [Required]
        public string Username { get; set; }

        [Required]  // Required when creating
        public string PasswordHash { get; set; }
    }

    public class NutritionistUpdateDto
    {
        public string? Id { get; set; } // Pode ser usado para identificar o nutricionista a ser atualizado
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        // outros campos opcionais
    }


}
