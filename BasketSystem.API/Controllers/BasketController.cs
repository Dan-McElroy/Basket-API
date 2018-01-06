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
        private IBasket Basket { get; set; }
        
        public BasketController(IBasket basket)
        {
            Basket = basket;
        }

        /// <summary>
        /// Adds an item by its unique ID to the basket.
        /// </summary>
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
        [HttpPost("item-id/{id}/quantity/{quantity}")]
        public void AddItem(string id, int quantity = 1)
        {
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity),
                    quantity, "Quantity must be greater than 0.");
            }
            Basket.AddItem(id, quantity);
        }

        /// <summary>
        /// Changes the quantity of an item in the basket.
        /// </summary>
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
        [HttpPut("item-id/{id}/quantity/{quantity}")]
        public void EditItemQuantity(string id, int quantity)
            => Basket.EditItemQuantity(id, quantity);


        /// <summary>
        /// Remove an item from the basket.
        /// </summary>
        /// <param name="id">The ID of the item to remove.</param>
        /// <remarks>
        /// If the item does not exist in the basket, this method will return
        /// sucessfully regardless.
        /// </remarks>
        [HttpDelete]
        [HttpDelete("item-id/{id}")]
        public void RemoveItem(string id)
            => Basket.RemoveItem(id);

        /// <summary>
        /// Clears the basket of all items.
        /// </summary>
        /// <remarks>
        /// If the basket is already empty, this method will return succesfully
        /// regardless.
        /// </remarks>
        [HttpDelete("all")]
        public void ClearBasket()
            => Basket.Clear();
    }
}