using System.ComponentModel.DataAnnotations;

namespace PieShopApi.Models
{
    public class Pie
    {
        public int Id { get; set; }

        [Required]
        [Length(3, 50)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
