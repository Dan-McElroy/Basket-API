using System;

namespace BasketSystem.Client.DTOs
{
    /// <summary>
    /// Represents the body of an item-related request
    /// to the Basket API.
    /// </summary>
    public class BasketItemRequest : BasketRequest
    {
        /// <summary>
        /// The ID of the item related to the request.
        /// </summary>
        public string Id { get; set; }
    }
}
