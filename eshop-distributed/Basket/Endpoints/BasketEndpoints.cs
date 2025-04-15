namespace Basket.Endpoints
{
    public static class BasketEndpoints
    {
        public static void MapBasketEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("basket");

            ////////////////////////////////////////////////////////////////////////////////////
            // GET SHOPPING CART FOR USERNAME   
            ////////////////////////////////////////////////////////////////////////////////////
            group.MapGet("/{userName}", async (string userName, BasketService basketService) =>
            {
                var shoppingCart = await basketService.GetBasket(userName);
                if (shoppingCart is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(shoppingCart);
            })
            .WithName("GetBasket")
            .Produces<ShoppingCart>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

            ////////////////////////////////////////////////////////////////////////////////////
            // CREATE OR UPDATE SHOPPING CART (UPSERT)
            ////////////////////////////////////////////////////////////////////////////////////
            group.MapPost("/", async (ShoppingCart shoppingCart, BasketService basketService) =>
            {
                await basketService.UpdateBasket(shoppingCart);
                return Results.Created("GetBasket", shoppingCart);
            })
            .WithName("UpdateBasket")
            .Produces<ShoppingCart>(StatusCodes.Status201Created)
            .RequireAuthorization();

            ////////////////////////////////////////////////////////////////////////////////////
            // DELETE SHOPPING CART(UPSERT)
            ////////////////////////////////////////////////////////////////////////////////////
            group.MapDelete("/{userName}", async (string userName, BasketService basketService) =>
            {
                await basketService.DeleteBasket(userName);
                return Results.NoContent();
            })
            .WithName("DeleteBasket")
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuthorization();
        }
    }
}
