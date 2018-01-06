using System;

namespace BasketSystem.Client.DTOs
{
    /// <summary>
    /// Represents the body of a request to the Basket API which alters the 
    /// quantity of an item.
    /// </summary>
    internal class BasketRequest
    {
        /// <summary>
        /// The unique token to access the user's basket.
        /// </summary>
        public Guid Token { get; set; }

        /// <summary>
        /// The ID of the item to be altered in the basket.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The quantity of the item to be updated.
        /// </summary>
        public int Quantity { get; set; }

        public BasketRequest(Guid token, string id, int quantity)
        {
            Token = token;
            Id = id;
            Quantity = quantity;
        }
    }
}
