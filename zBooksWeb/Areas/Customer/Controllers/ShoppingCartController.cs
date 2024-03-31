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
        //_unitOfWork.ShoppingCart.Add(shoppingCart);
        //_unitOfWork.Save();
        return View(_unitOfWork);
    }
    
    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity)
    {
        var cartItem = _unitOfWork.CartItem.Get(u => u.CartId == User.Identity.Name && u.ProductId == productId);
        if (cartItem is null)
        {
            _unitOfWork.CartItem.Add(new CartItem { ProductId = productId, Quantity = quantity, CartId = User.Identity.Name });
            _unitOfWork.Save();
        }
        else
        {
            cartItem.Quantity += quantity;
        }
        //_unitOfWork.Save();
        // Redirect to shopping cart view or other appropriate page
        return RedirectToAction("Index", "ShoppingCart");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}