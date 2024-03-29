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

    public IActionResult Index()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
        return View(productList);
    }
    
    public IActionResult Details(int productId)
    {
        var product = _unitOfWork.Product.Get(u=> u.Id == productId, includeProperties: "Category");
        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}