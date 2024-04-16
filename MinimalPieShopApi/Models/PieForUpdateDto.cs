using System.ComponentModel.DataAnnotations;

namespace MinimalPieShopApi.Models
{
    public class PieForUpdateDto
    {
        [Required]
        [Length(3, 50)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        public string Category { get; set; } = "Other";
    }
}
