﻿
using System.Net;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            //Get Basket If not exist create new basket with default logged in user name
            var userName = "Vijay";
            ShoppingCartModel basket;
            try
            {
                var getBasketResponse = await GetBasket(userName);
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
            {
                {
                    basket = new ShoppingCartModel
                    {
                        UserName = userName,
                        Items = []
                    };
                }                
            }
            return basket;
        }
    }
}
