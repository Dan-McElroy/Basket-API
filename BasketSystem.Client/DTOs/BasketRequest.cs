using System;

namespace BasketSystem.Client.DTOs
{
    /// <summary>
    /// Represents the body of a request to the Basket API.
    /// </summary>
    public class BasketRequest
    {
        /// <summary>
        /// The user token of the basket related to the
        /// request.
        /// </summary>
        public Guid UserToken { get; set; }
    }
}
