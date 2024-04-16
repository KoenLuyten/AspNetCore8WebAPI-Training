using System.ComponentModel.DataAnnotations;

namespace MinimalPieShopApi.Models
{
    public class PieForListDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string Category { get; set; } = "Other";
    }
}
