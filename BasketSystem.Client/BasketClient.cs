using System;
using System.Net.Http;

namespace BasketSystem.Client
{
    public class BasketClient : IDisposable
    {
        private HttpClient _client;

        public BasketClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("TODO: Get from settings")
            };
        }

        public void AddItem(string itemId, int quantity)
        {

        }

        public void EditItemQuantity(string itemId, int quantity)
        {

        }

        public void RemoveItem(string itemId)
        {

        }

        public void ClearBasket()
        {

        }

        #region IDisposable Methods

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion

        private void SendRequest()
        {

        }
    }
}