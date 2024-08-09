﻿
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {

                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("StoereBasketRequest")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Stoere Basket Request")
            .WithDescription("Stoere Basket Request");

        }
    }
}
