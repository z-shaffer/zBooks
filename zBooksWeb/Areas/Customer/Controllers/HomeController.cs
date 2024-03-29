using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooksWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IShoppingCartManager shoppingCartManager) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IShoppingCartManager _shoppingCartManager = shoppingCartManager;

    public IActionResult Index()
    {
        var shoppingCart = TryBuildShoppingCart();
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
        return View(productList);
    }
    
    public IActionResult Details(int productId)
    {
        var product = _unitOfWork.Product.Get(u=> u.Id == productId, includeProperties: "Category");
        return View(product);
    }
    
    public IActionResult ShoppingCart(string userId)
    {
        var shoppingCart = TryBuildShoppingCart();
        // Schedule disposal after 15 minutes
        _shoppingCartManager.ScheduleDispose(shoppingCart);
        return View(shoppingCart);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public ShoppingCart TryBuildShoppingCart()
    {
        var userId = User.Identity?.Name;
        var shoppingCart = _unitOfWork.ShoppingCart.Get(u => u.UserId == userId);
        if (shoppingCart is null)
        {
            // Create a new shopping cart
            shoppingCart = new ShoppingCart
            {
                UserId = userId,
                DateCreated = DateTime.Now,
                CartItems = new List<CartItem>()
            };
            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
        }
        return shoppingCart;
    }
}