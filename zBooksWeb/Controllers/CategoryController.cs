using Microsoft.AspNetCore.Mvc;

namespace zBooksWeb.Controllers;

public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}