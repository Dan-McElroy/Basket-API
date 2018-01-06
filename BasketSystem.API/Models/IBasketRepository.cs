using BasketSystem.Models;
using System;

namespace BasketSystem.API.Models
{
    /// <summary>
    /// Repository to store all active user baskets.
    /// </summary>
    public interface IBasketRepository
    {
        /// <summary>
        /// Creates a new basket.
        /// </summary>
        /// <returns>The unique token to access this basket.</returns>
        Guid CreateBasket();

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
        IBasket FindBasket(Guid userToken);

        /// <summary>
        /// Removes a basket from the repository.
        /// </summary>
        /// <param name="userToken">
        /// The user token of the repository to be removed.
        /// </param>
        void RemoveBasket(Guid userToken);
    }
}
