using Microsoft.AspNetCore.Mvc;

namespace CheckoutAPI.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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