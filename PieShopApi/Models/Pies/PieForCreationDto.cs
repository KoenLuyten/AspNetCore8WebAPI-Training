using System.ComponentModel.DataAnnotations;

namespace PieShopApi.Models.Pies
{
    public class PieForCreationDto
    {
        [Required]
        [Length(3, 50)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        [Required]
        public string Category { get; set; } = "Other";
    }
}
