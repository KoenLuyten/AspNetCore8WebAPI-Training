namespace PieShopApi.Filters
{
    public class AuditEntry
    {
        public AuditEntry(string message)
        {
            Message = message;
            CreatedAt = DateTime.Now;
        }
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
