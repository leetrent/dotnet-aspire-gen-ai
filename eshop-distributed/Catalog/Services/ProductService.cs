namespace Catalog.Services
{
    public class ProductService(ProductDbContext dbContext)
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product updateProduct, Product inputProduct)
        {
            updateProduct.Name = inputProduct.Name;
            updateProduct.Description = inputProduct.Description;
            updateProduct.Price = inputProduct.Price;
            updateProduct.ImageUrl = inputProduct.ImageUrl;
            
            dbContext.Products.Update(updateProduct);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product deleteProduct)
        {
            dbContext.Products.Remove(deleteProduct);
            await dbContext.SaveChangesAsync();
        }
    }
}
