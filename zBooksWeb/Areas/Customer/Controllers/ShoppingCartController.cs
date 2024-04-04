using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;
using zBooks.Utility;

namespace zBooksWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = StaticDetails.Role_Customer)]
public class ShoppingCartController(ILogger<ShoppingCartController> logger, IUnitOfWork unitOfWork, 
    IShoppingCartManager shoppingCartManager) : Controller
{
    private readonly ILogger<ShoppingCartController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IShoppingCartManager _shoppingCartManager = shoppingCartManager;

    public IActionResult Index()
    {
        var shoppingCart = _shoppingCartManager.TryBuildShoppingCart(User.Identity.Name);
        return View(_unitOfWork);
    }
    
    // Increase the amount of said product in the user's shopping cart by that amount
    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity)
    {
        var cartItem = _unitOfWork.CartItem.Get(u => u.CartId == User.Identity.Name && u.ProductId == productId);
        if (cartItem is null)
        {
            _unitOfWork.CartItem.Add(new CartItem { ProductId = productId, Quantity = quantity, CartId = User.Identity.Name });
        }
        else
        {
            cartItem.Quantity += quantity;
            _unitOfWork.CartItem.Update(cartItem);
        }
        _unitOfWork.Save();
        TempData["success"] = "Success: Item added to cart";
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    // Delete the provided item based on ID
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var itemToBeDeleted = _unitOfWork.CartItem.Get(u => u.Id == id);
        // If the product is found, remove it. Else throw an error
        if (itemToBeDeleted is not null)
        {
            _unitOfWork.CartItem.Remove(itemToBeDeleted);
            _unitOfWork.Save();
            TempData["success"] = "Success: Item removed from cart";
        }
        else
        {
            TempData["error"] = "Error: Item not found";
        }
        return Json(Url.Action("Index", "ShoppingCart")); 
    }

    [HttpDelete]
    public IActionResult ClearCart()
    {
        var itemsToBeDeleted = _unitOfWork.CartItem.GetAll().Where(u=>u.CartId == User.Identity.Name);
        if (itemsToBeDeleted is not null)
        {
            foreach (var item in itemsToBeDeleted)
            {
                _unitOfWork.CartItem.Remove(item);
                _unitOfWork.Save();
                
            }
            TempData["success"] = "Success: Cart cleared";
        }
        else
        {
            TempData["error"] = "Error: Unable to clear cart";
        }
        return Json(Url.Action("Index", "ShoppingCart")); 
    }

    public IActionResult Checkout()
    {
        return View(_unitOfWork);
    }
}