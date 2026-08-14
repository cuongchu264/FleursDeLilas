namespace FleursDeLilas.API.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = null!;
        public decimal OrderPrice { get; set; }
        public decimal OrderShipPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderItemDto[] Flowers { get; set; } = Array.Empty<OrderItemDto>();
        public OrderItemDto[] Supplies { get; set; } = Array.Empty<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateOrderFlowerDto
    {
        public int Id { get; set; }
        public int Qty { get; set; }
    }

    public class CreateOrderSupplyDto
    {
        public int Id { get; set; }
        public int Qty { get; set; }
    }

    public class CreateOrderDto
    {
        public string? OrderName { get; set; }
        public decimal? OrderShipPrice { get; set; }
        public CreateOrderFlowerDto[] Flowers { get; set; } = Array.Empty<CreateOrderFlowerDto>();
        public CreateOrderSupplyDto[] Supplies { get; set; } = Array.Empty<CreateOrderSupplyDto>();
    }
}
