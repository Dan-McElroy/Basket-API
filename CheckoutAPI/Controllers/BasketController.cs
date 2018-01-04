using Microsoft.AspNetCore.Mvc;
using System;

namespace CheckoutAPI.Controllers
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
        /// Adds an item by its unique ID to the basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to be added.</param>
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
        [HttpPost("item-id/{itemId}/quantity/{quantity}")]
        public void AddItem(string itemId, int quantity = 1)
        {
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), 
                    quantity, "Quantity must be greater than 0.");
            }
        }

        /// <summary>
        /// Changes the quantity of an item in the store.
        /// </summary>
        /// <param name="itemId">
        /// The ID of the item to edit the quantity of.
        /// </param>
        /// <param name="quantity">The new quantity for the item.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the item with the provided ID does not already exist in
        /// the store.
        /// </exception>
        [HttpPut]
        [HttpPut("item-id/{itemId}/quantity/{quantity}")]
        public void EditItemQuantity(string itemId, int quantity)
        {
            
        }

        /// <summary>
        /// Remove an item from the basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to remove.</param>
        /// <remarks>
        /// If the item does not exist in the basket, this method will return
        /// sucessfully regardless.
        /// </remarks>
        [HttpDelete]
        [HttpDelete("item-id/{itemId}")]
        public void RemoveItem(string itemId)
        {
            
        }

        /// <summary>
        /// Clears the basket of all items.
        /// </summary>
        /// <remarks>
        /// If the basket is already empty, this method will return succesfully
        /// regardless.
        /// </remarks>
        [HttpDelete("all")]
        public void ClearBasket()
        {
            
        }
    }
}