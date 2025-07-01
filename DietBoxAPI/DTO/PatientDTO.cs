using System.ComponentModel.DataAnnotations;

namespace DietBoxAPI.DTO
{
    public class PatientCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class PatientUpdateDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    public class PatientReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
