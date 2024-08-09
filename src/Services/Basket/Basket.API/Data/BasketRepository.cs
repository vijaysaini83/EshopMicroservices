﻿
namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName,cancellationToken);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
