namespace BasketSystem.Client.DTOs
{
    /// <summary>
    /// Represents the body of a request to the Basket API which alters the 
    /// quantity of an item.
    /// </summary>
    public class BasketItemQuantityRequest : BasketItemRequest
    {
        /// <summary>
        /// The quantity of the item to be updated.
        /// </summary>
        public int Quantity { get; set; }
    }
}
