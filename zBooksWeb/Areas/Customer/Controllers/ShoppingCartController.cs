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

    #region API CALLS
    
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var itemToBeDeleted = _unitOfWork.CartItem.Get(u => u.Id == id);
        // If the product is not found, throw an error
        if (itemToBeDeleted is null)
        {
            return Json(new { success = false, message = "Delete Error: Product is null" });
        }
        _unitOfWork.CartItem.Remove(itemToBeDeleted);
        _unitOfWork.Save();
        var objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { success = true, message = "Success: Cart item deleted" });
    }

    #endregion
    
}