using BasketSystem.API.Models;
using BasketSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    /// <summary>
    /// Controller for all basket-related operations.
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController : Controller
    {
        /// <summary>
        /// The basket for the system.
        /// </summary>
        private IBasketRepository Repository { get; set; }
        
        public BasketController(IBasketRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Creates a new <see cref="Basket"/> for the User.
        /// </summary>
        /// <returns>The token required to access this basket.</returns>
        [HttpPost("new")]
        public Guid CreateBasket()
            => Repository.CreateBasket();

        /// <summary>
        /// Adds an item by its unique ID to the user's basket.
        /// </summary>
        /// <param name="token">The user's access token.</param>
        /// <param name="id">The ID of the item to be added.</param>
        /// <param name="quantity">
        /// An optional quantity for the item - the default is 1.
        /// </param>
        /// <remarks>
        /// If the item already exists in the basket, the existing quantity is
        /// increased by the amount specified in the request, or 1 if no
        /// quantity is provided.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the quantity provided is below 1.
        /// </exception>
        [HttpPost]
        [HttpPost("user-token/{token:guid}/item-id/{id}/quantity/{quantity}")]
        public BasketItem AddItem(Guid token, string id, int quantity = 1)
        {
            var basket = FindUserBasket(token);
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity),
                    quantity, "Quantity must be greater than 0.");
            }
            return basket.AddItem(id, quantity);
        }

        /// <summary>
        /// Changes the quantity of an item in the user's basket.
        /// </summary>
        /// <param name="token">The user's access token.</param>
        /// <param name="id">
        /// The ID of the item to edit the quantity of.
        /// </param>
        /// <param name="quantity">The new quantity for the item.</param>
        /// <remarks>
        /// If the new quantity of the item is below 1, the item will
        /// be removed from the basket.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the item with the provided ID does not already exist in
        /// the basket.
        /// </exception>
        [HttpPut]
        [HttpPut("user-token/{token:guid}/item-id/{id}/quantity/{quantity}")]
        public BasketItem EditItemQuantity(Guid token, string id, int quantity)
        {
            var basket = FindUserBasket(token);
            return basket.EditItemQuantity(id, quantity);
        }


        /// <summary>
        /// Remove an item from the basket.
        /// </summary>
        /// <param name="id">The ID of the item to remove.</param>
        /// <remarks>
        /// If the item does not exist in the basket, this method will return
        /// sucessfully regardless.
        /// </remarks>
        [HttpDelete]
        [HttpDelete("user-token/{token:guid}/item-id/{id}")]
        public void RemoveItem(Guid token, string id)
        {
            var basket = FindUserBasket(token);
            basket.RemoveItem(id);
        }

        /// <summary>
        /// Clears the user's basket of all items.
        /// </summary>
        /// <remarks>
        /// If the basket is already empty, this method will return succesfully
        /// regardless.
        /// </remarks>
        [HttpDelete("all-items")]
        [HttpDelete("user-token/{token:guid}/all-items")]
        public void ClearBasket(Guid token)
        {
            var basket = FindUserBasket(token);
            basket.Clear();
        }

        [NonAction]
        public IBasket FindUserBasket(Guid userToken)
        {
            var basket = Repository.FindBasket(userToken);
            if (basket == null)
            {
                throw new InvalidOperationException(
                    "Basket for the specified token does not exist.");
            }
            return basket;
        }
    }
}