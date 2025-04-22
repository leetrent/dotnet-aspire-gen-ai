using Catalog.Services;

namespace Catalog.Endpoints
{
    public static class ProductEndpoints
    {
        public static string GetAllProducts = "GetAllProducts";
        public static string GetProductById = "GetProductById";
        public static string CreateProduct = "CreateProduct";
        public static string UpdateProduct = "UpdateProduct";
        public static string DeleteProduct = "DeleteProduct";
        public static string GetProductsByCategory = "GetProductsByCategory";
    
        public static void MapProductEndpoints(this IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder groupBuilder = builder.MapGroup("/products");

            /////////////////////////////////////////////////////////////////////
            /// GET ALL PRODUCTS
            /////////////////////////////////////////////////////////////////////
            groupBuilder.MapGet("/", async (ProductService service) =>
            {
                IEnumerable<Product> products = await service.GetProductsAsync();
                return Results.Ok(products);
            })
            .WithName(GetAllProducts)
            .Produces<List<Product>>(StatusCodes.Status200OK);

            /////////////////////////////////////////////////////////////////////
            /// GET PRODUCT BY ID
            /////////////////////////////////////////////////////////////////////
            groupBuilder.MapGet("/{id}", async (int id, ProductService service) =>
            {
                Product? product = await service.GetProductByIdAsync(id);
                if (product == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(product);
            })
            .WithName(GetProductById)
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            /////////////////////////////////////////////////////////////////////
            /// CREATE PRODUCT
            /////////////////////////////////////////////////////////////////////
            groupBuilder.MapPost("/", async (Product product, ProductService service) =>
            {
                await service.CreateProductAsync(product);
                return Results.Created($"/products/{product.Id}", product);
            })
            .WithName(CreateProduct)
            .Produces<Product>(StatusCodes.Status201Created);


            /////////////////////////////////////////////////////////////////////
            /// UPDATE PRODUCT
            /////////////////////////////////////////////////////////////////////
            groupBuilder.MapPut("/{id}", async (int id, Product inputProduct, ProductService service) =>
            {
                Product? updateProduct = await service.GetProductByIdAsync(id);
                if (updateProduct == null)
                {
                    return Results.NotFound();
                }
                await service.UpdateProductAsync(updateProduct, inputProduct);
                return Results.NoContent();
            })
            .WithName(UpdateProduct)
            .Produces<Product>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            /////////////////////////////////////////////////////////////////////
            /// DELETE PRODUCT
            /////////////////////////////////////////////////////////////////////
            groupBuilder.MapDelete("/{id}", async (int id, ProductService service) =>
            {
                Product? deleteProduct = await service.GetProductByIdAsync(id);
                if (deleteProduct == null)
                {
                    return Results.NotFound();
                }
                await service.DeleteProductAsync(deleteProduct);
                return Results.NoContent();
            })
            .WithName(DeleteProduct)
            .Produces<Product>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            /////////////////////////////////////////////////////////////////////
            /// AI SUPPORT
            /////////////////////////////////////////////////////////////////////
            groupBuilder.MapGet("/support/{query}", async (string query, ProductAIService service) =>
            {
                var response = await service.SupportAsync(query);
                return Results.Ok(response);    
            })
            .WithName("Support")
            .Produces(StatusCodes.Status200OK);
        }

    }
}
