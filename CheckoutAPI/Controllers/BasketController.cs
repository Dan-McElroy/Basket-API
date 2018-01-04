using Microsoft.AspNetCore.Mvc;

namespace CheckoutAPI.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController : Controller
    {
        /// <summary>
        /// Adds an item by its unique ID to the basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to be added.</param>
        /// <param name="quantity">An optional quantity for the item - the default is 1.</param>
        /// <remarks>
        /// If the item already exists in the basket, the existing quantity is increased by the 
        /// amount specified in the request, or 1 if no quantity is provided.
        /// </remarks>
        [HttpPost]
        public void AddItem(string itemId, int quantity = 1)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId">The ID of the item to edit.</param>
        /// <param name="quantity">The new quantity for the item.</param>
        [HttpPut]
        public void EditItemQuantity()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        [HttpDelete]
        public void DeleteItem(string itemId)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpDelete("all")]
        public void ClearBasket()
        {
            
        }
    }
}