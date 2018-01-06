namespace BasketSystem.Client
{
    /// <summary>
    /// An object representing the settings related to the BasketAPI client.
    /// </summary>
    public class Settings
    {
        public const string TOKENINSERT = "$TOKEN";
        public const string IDINSERT = "$ID";

        /// <summary>
        /// The base URL of the Basket API.
        /// </summary>
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// The desired API version to use.
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// The route to the basket creation endpoint.
        /// </summary>
        public string BasketCreationEndpoint { get; set; }
        
        /// <summary>
        /// The route to the endpoint which removes an item from a basket.
        /// </summary>
        public string RemoveItemEndpoint { get; set; }

        /// <summary>
        /// The route to the endpoint which clears all items from a basket.
        /// </summary>
        public string ClearItemsEndpoint { get; set; }
    }
}
