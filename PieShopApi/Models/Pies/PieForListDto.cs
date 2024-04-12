namespace PieShopApi.Models.Pies
{
    public class PieForListDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
