using System.ComponentModel.DataAnnotations;
using PieShopApi.Models.Allergies;

namespace PieShopApi.Models.Pies
{
    public class Pie
    {
        public Pie()
        {
            AllergyItems = new List<Allergy>();
        }

        public int Id { get; set; }

        [Required]
        [Length(3, 50)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        public ICollection<Allergy> AllergyItems { get; set; }
    }
}
