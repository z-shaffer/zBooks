using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;
using zBooks.Utility;

namespace zBooksWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = StaticDetails.Role_Customer)]
public class ShoppingCartController(ILogger<ShoppingCartController> logger, IUnitOfWork unitOfWork, IShoppingCartManager shoppingCartManager) : Controller
{
    private readonly ILogger<ShoppingCartController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IShoppingCartManager _shoppingCartManager = shoppingCartManager;

    public IActionResult Index()
    {
        var shoppingCart = _shoppingCartManager.TryBuildShoppingCart(User.Identity.Name);
        return View(shoppingCart);
    }
    
    [HttpPost]
    public IActionResult AddToCart(Product product, int quantity)
    {
        var cartItems = _unitOfWork.ShoppingCart.Get(u => u.UserId == User.Identity.Name).CartItems;
        if (cartItems == null)
        {
            cartItems = new List<CartItem>();
        }
        bool productAlreadyInCart = cartItems.Any(item =>
        {
            if (item.Product == product)
            {
                item.Quantity += quantity;
                return true;
            }
            return false;
        });

        if (!productAlreadyInCart)
        {
            cartItems.Add(new CartItem { Product = product, Quantity = quantity });
        }

        return RedirectToAction("Index", "ShoppingCart");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}