using System.Security.Claims;
using zBooks.Models;

namespace zBooks.DataAccess.Repository.IRepository;

public interface IShoppingCartManager : IManager<ShoppingCartManager>
{
    void ScheduleDispose(ShoppingCart obj);
    int GetCartCount(String userId);
}