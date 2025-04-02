namespace Basket.Models
{
    public class ShoppingCart
    {
        public required string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}
