namespace WebApi.DTO
{
    public class WaterColorResponse
    {
        public string PaintingId { get; set; }
        public string PaintingName { get; set; }
        public string Description { get; set; }
        public string PaintingAuthor { get; set; }
        public decimal? Price { get; set; }
        public int? PublishYear { get; set; }
        public string StyleName { get; set; }
    }
}
