using Microsoft.AspNetCore.Mvc;

namespace CheckoutAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Basket")]
    public class BasketController : Controller
    {
        [HttpPost]
        public void AddItem()
        {

        }

        [HttpPut]
        public void EditItem()
        {
            
        }

        [HttpDelete]
        public void DeleteItem()
        {
            
        }

        [HttpDelete("all")]
        public void ClearBasket()
        {
            
        }
    }
}