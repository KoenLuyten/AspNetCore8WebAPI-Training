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
        public IList<string> AllergyItems { get; set; }
    }
}
