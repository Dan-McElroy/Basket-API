using BasketSystem.Client.DTOs;
using BasketSystem.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasketSystem.Client
{
    /// <summary>
    /// A Disposable client that interfaces with the Basket API.
    /// </summary>
    public class BasketClient : IDisposable
    {
        /// <summary>
        /// The <see cref="HttpClient"/> to send requests to the Basket API.
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// The unique token used to access the user's basket.
        /// </summary>
        public readonly Guid UserToken;

        /// <summary>
        /// The section of a  request string that specifies the user token.
        /// </summary>
        private string TokenURLSegment => $"user-token/{UserToken}";

        /// <summary>
        /// Creates a new client, either for an existing basket or a new one.
        /// </summary>
        /// <param name="userToken">The token of an existing basket.</param>
        /// <remarks>
        /// If a token is not provided, then the client will create a new
        /// basket on the API and get a new token.
        /// </remarks>
        public BasketClient(Guid? userToken = null)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:52349/api/v1/basket")
            };
            UserToken = userToken ?? Task.Run(CreateNewBasket).Result;
        }

        /// <summary>
        /// Adds an item by its unique ID to the Basket.
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
        public async Task<BasketItem> AddItemAsync(string itemId, int quantity = 1)
            => await SendBasketItemRequestAsync(
                itemId, quantity, _client.PostAsync);

        /// <summary>
        /// Changes the quantity of an item in the Basket.
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
        /// The edited <see cref="BasketItem"/>, or null if it has been 
        /// removed.
        /// </returns>
        public async Task<BasketItem> EditItemQuantityAsync(string itemId, 
            int quantity)
            => await SendBasketItemRequestAsync(
                itemId, quantity, _client.PutAsync);

        /// <summary>
        /// Remove an item from the basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to remove.</param>
        public async Task RemoveItemAsync(string itemId)
        {
            await _client.DeleteAsync($"{TokenURLSegment}/item-id/{itemId}");
        }

        /// <summary>
        /// Clears the Basket of all items.
        /// </summary>
        public async Task ClearBasketAsync()
        {
            await _client.DeleteAsync($"{TokenURLSegment}/all-items");
        }

        #region IDisposable Methods

        /// <summary>
        /// Disposes the client, terminating all open connections.
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a new basket for the client.
        /// </summary>
        /// <returns></returns>
        private async Task<Guid> CreateNewBasket()
        {
            var response = await _client.PostAsync("new-basket", null);
            return Guid.Parse(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Defines a method which sends an HTTP request to a given endpoint, 
        /// and returns a response.
        /// </summary>
        /// <param name="requestUri">The endpoint to send a request to.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>The response to the request.</returns>
        private delegate Task<HttpResponseMessage> HttpRequest(
            string requestUri, HttpContent content);

        /// <summary>
        /// A helper method to send a request with an item ID and quantity to
        /// the Basket API, which is expected to return a 
        /// <see cref="BasketItem"/>.
        /// </summary>
        /// <param name="itemId">
        /// The ID of the item related to the request.
        /// </param>
        /// <param name="quantity">
        /// The quantity of the item in question.
        /// </param>
        /// <param name="request">
        /// The request method which should be used.
        /// </param>
        /// <returns>
        /// The <see cref="BasketItem"/> related to the request.
        /// </returns>
        private async Task<BasketItem> SendBasketItemRequestAsync(
            string itemId, int quantity, HttpRequest request)
        {
            var response = await request(string.Empty, 
                GetRequestContent(itemId, quantity));

            return JsonConvert.DeserializeObject<BasketItem>(
                await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Creates request content from an item ID and quantity.
        /// </summary>
        /// <param name="itemId">The item ID related to the request.</param>
        /// <param name="quantity">The quantity related to the request.</param>
        /// <returns></returns>
        private HttpContent GetRequestContent(string itemId, int quantity)
            => new JsonContent(
                new BasketRequest(UserToken, itemId, quantity));

        #endregion
    }
}