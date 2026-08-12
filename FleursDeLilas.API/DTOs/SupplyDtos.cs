namespace FleursDeLilas.API.DTOs
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateSupplyDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? Note { get; set; }
    }

    public class UpdateSupplyDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? Note { get; set; }
    }
}
