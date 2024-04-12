namespace PieShopApi.Models.Pies
{
    public class PieDto
    {
        public PieDto()
        {
            AllergyItems = new List<string>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public string Category { get; set; } = "Other";
        public IList<string> AllergyItems { get; set; }
    }
}
