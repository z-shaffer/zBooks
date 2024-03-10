using Microsoft.AspNetCore.Mvc;
using zBooksWeb.Data;
using zBooksWeb.Models;

namespace zBooksWeb.Controllers;

public class CategoryController(ApplicationDbContext db) : Controller
{
    private readonly ApplicationDbContext _db = db;

    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
}