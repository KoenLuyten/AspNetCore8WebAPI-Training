using PieShopApi.Models.Pies;

namespace PieShopApi.Models.Allergies
{
    public class Allergy
    {
        public Allergy()
        {
            Pies = new List<Pie>();
        }

        public required int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Pie> Pies { get; set; }
    }
}
