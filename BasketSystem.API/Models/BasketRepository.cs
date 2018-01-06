using BasketSystem.Models;
using System;
using System.Collections.Generic;

namespace BasketSystem.API.Models
{
    public class BasketRepository : IBasketRepository
    {
        /// <summary>
        /// The collection of active baskets.
        /// </summary>
        /// <remarks>
        /// The keys are unique user tokens,
        /// and the values are the corresponding <see cref="Basket"/>s.
        /// </remarks>
        private Dictionary<Guid, IBasket> Baskets { get; }

        public BasketRepository()
        {
            Baskets = new Dictionary<Guid, IBasket>();
        }

        /// <summary>
        /// Creates a new basket.
        /// </summary>
        /// <returns>The unique token to access this basket.</returns>
        public Guid CreateBasket()
        {
            var userToken = Guid.NewGuid();
            var basket = new Basket();
            Baskets.Add(userToken, basket);
            return userToken;
        }

        /// <summary>
        /// Finds a basket for a given user token.
        /// </summary>
        /// <param name="userToken">
        /// The user token associated with the desired basket.
        /// </param>
        /// <returns>
        /// The basket with the given token, or <see cref="null"/>
        /// if no such basket exists.
        /// </returns>
        public IBasket FindBasket(Guid userToken)
            => Baskets.GetValueOrDefault(userToken);

        /// <summary>
        /// Removes a basket from the repository.
        /// </summary>
        /// <param name="userToken">
        /// The user token of the repository to be removed.
        /// </param>
        public void RemoveBasket(Guid userToken)
            => Baskets.Remove(userToken);
    }
}
