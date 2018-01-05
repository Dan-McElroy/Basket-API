using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutAPI.Models
{
    /// <summary>
    /// A basket of items to be ordered.
    /// </summary>
    public class Basket : IBasket
    {
        // TODO Make threadsafe.
        private ICollection<BasketItem> Items { get; }

        /// <summary>
        /// Default constructor for <see cref="Basket"/>.
        /// </summary>
        public Basket()
        {
            Items = new List<BasketItem>();
        }

        #region IBasket Methods

        /// <summary>
        /// Adds an item by its unique ID to the basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to be added.</param>
        /// <param name="quantity">
        /// The quantity of the item.
        /// </param>
        /// <remarks>
        /// If the item already exists in the basket, the existing quantity is
        /// increased by the amount specified.
        /// </remarks>
        public void AddItem(string itemId, int quantity)
        {
        }

        /// <summary>
        /// Changes the quantity of an item in the basket.
        /// </summary>
        /// <param name="itemId">
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
        public void Clear()
        {
        }
    }
}
