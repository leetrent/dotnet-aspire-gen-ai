namespace Basket.Models
{
    public class ShoppingCartItem
    {
        public required int ProductId { get; set; } = default;
        public required int Quantity { get; set; } = default;
        public required decimal Price { get; set; } = default;
        public required string? ProductName { get; set; } = default;
        public string? Color { get; set; } = default;

    }
}
