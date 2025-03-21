namespace Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public required string Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public required string ImageUrl { get; set; }

    }
}
