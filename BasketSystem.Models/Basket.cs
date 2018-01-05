using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BasketSystem.Models.Tests")]
namespace BasketSystem.Models
{
    /// <summary>
    /// A basket of items to be ordered.
    /// </summary>
    public class Basket : IBasket
    {
        // TODO Make threadsafe.
        /// <summary>
        /// A collection of items in the <see cref="Basket"/>.
        /// </summary>
        internal ICollection<BasketItem> Items { get; }

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
            var existingItem = FindById(itemId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return;
            }
            Items.Add(new BasketItem(itemId, quantity));
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
            var existingItem = FindById(itemId);
            if (existingItem == null)
            {
                throw new InvalidOperationException(
                    "No such item exists in the basket.");
            }
            try
            {
                existingItem.Quantity = quantity;
            }
            catch (ArgumentOutOfRangeException)
            {
                Items.Remove(existingItem);
            }
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
            => Items.Remove(FindById(itemId));

        /// <summary>
        /// Clears the basket of all items.
        /// </summary>
        /// <remarks>
        /// If the basket is already empty, this method will return succesfully
        /// regardless.
        /// </remarks>
        public void Clear()
            => Items.Clear();

        #endregion

        #region Internal Methods

        internal BasketItem FindById(string itemId)
            => Items.FirstOrDefault(item => item.Id == itemId);

        #endregion
    }
}