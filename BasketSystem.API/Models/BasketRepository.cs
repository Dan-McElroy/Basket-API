using BasketSystem.Models;
using System;
using System.Collections.Generic;

namespace BasketSystem.API.Models
{
    public class BasketRepository : IBasketRepository
    {
        private Dictionary<Guid, IBasket> Baskets { get; }

        public BasketRepository()
        {
            Baskets = new Dictionary<Guid, IBasket>();
        }

        public Guid CreateBasket()
        {
            var userToken = Guid.NewGuid();
            var basket = new Basket();
            Baskets.Add(userToken, basket);
            return userToken;
        }

        public IBasket FindBasket(Guid userToken)
            => Baskets.GetValueOrDefault(userToken);

        public void RemoveBasket(Guid userToken)
            => Baskets.Remove(userToken);
    }
}
