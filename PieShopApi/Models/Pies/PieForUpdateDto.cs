using System.ComponentModel.DataAnnotations;

namespace PieShopApi.Models.Pies
{
    public class PieForUpdateDto
    {
        [Required]
        [Length(3, 50)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }
    }
}
