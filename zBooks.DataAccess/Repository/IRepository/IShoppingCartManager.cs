using System.Security.Claims;
using zBooks.Models;

namespace zBooks.DataAccess.Repository.IRepository;

public interface IShoppingCartManager : IManager<ShoppingCartManager>
{
    public ShoppingCart TryBuildShoppingCart(string userId);
}