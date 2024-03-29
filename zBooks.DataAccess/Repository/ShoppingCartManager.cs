using System.Security.Claims;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class ShoppingCartManager(IUnitOfWork unitOfWork) : IShoppingCartManager
{
    public ShoppingCart TryBuildShoppingCart(string userId)
    {
        var shoppingCart = unitOfWork.ShoppingCart.Get(u => u.UserId == userId);
        if (shoppingCart is null)
        {
            // Create a new shopping cart
            shoppingCart = new ShoppingCart
            {
                UserId = userId,
                DateCreated = DateTime.Now
            };
            unitOfWork.ShoppingCart.Add(shoppingCart);
            unitOfWork.Save();
        }
        return shoppingCart;
    }
}