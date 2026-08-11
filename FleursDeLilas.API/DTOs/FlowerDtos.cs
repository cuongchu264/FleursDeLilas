namespace FleursDeLilas.API.DTOs
{
    public class FlowerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
        public int FailedCount { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateFlowerDto
    {
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
        public int FailedCount { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? Note { get; set; }
    }

    public class UpdateFlowerDto
    {
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
        public int FailedCount { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? Note { get; set; }
    }
}
