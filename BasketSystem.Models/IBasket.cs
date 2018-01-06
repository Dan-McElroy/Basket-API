namespace BasketSystem.Models
{
    public interface IBasket
    {
        /// <summary>
        /// Adds an item by its unique ID to the <see cref="Basket"/>.
        /// </summary>
        /// <param name="itemId">The ID of the item to be added.</param>
        /// <param name="quantity">
        /// The quantity of the item.
        /// </param>
        /// <remarks>
        /// If the item already exists in the basket, the existing quantity is
        /// increased by the amount specified.
        /// </remarks>
        /// <returns>
        /// The <see cref="BasketItem"/> added to the <see cref="Basket"/>.
        /// </returns>
        BasketItem AddItem(string itemId, int quantity);

        /// <summary>
        /// Changes the quantity of an item in the <see cref="Basket"/>.
        /// </summary>
        /// <param name="itemId">
        /// The ID of the item to edit the quantity of.
        /// </param>
        /// <param name="quantity">The new quantity for the item.</param>
        /// <remarks>
        /// If the new quantity of the item is below 1, the item will
        /// be removed from the basket.
        /// </remarks>
        /// <returns>
        /// The edited <see cref="BasketItem"/>, or <see cref="null"/>
        /// if it has been removed.
        /// </returns>
        BasketItem EditItemQuantity(string itemId, int quantity);

        /// <summary>
        /// Remove an item from the basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to remove.</param>
        void RemoveItem(string itemId);

        /// <summary>
        /// Clears the basket of all items.
        /// </summary>
        void Clear();
    }
}