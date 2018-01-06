using BasketSystem.Models;
using System;

namespace BasketSystem.API.Models
{
    public interface IBasketRepository
    {
        Guid CreateBasket();

        IBasket FindBasket(Guid userToken);

        void RemoveBasket(Guid userToken);
    }
}
