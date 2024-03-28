using System.Security.Claims;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class ShoppingCartManager(IUnitOfWork unitOfWork) : IShoppingCartManager
{
    public void ScheduleDispose(ShoppingCart shoppingCart)
    {
        var timer = new System.Threading.Timer((state) =>
        {
            unitOfWork.ShoppingCart.Remove(shoppingCart);
            unitOfWork.Save();
        }, null, TimeSpan.FromMinutes(15), TimeSpan.FromMilliseconds(-1));
    }

    public int GetCartCount(string userId)
    {
        var cartCount = unitOfWork.ShoppingCart.Get(u=> u.UserId == userId).CartItems.Count;
        return cartCount;
    }
}