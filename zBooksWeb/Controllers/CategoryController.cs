using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zBooksWeb.Data;
using zBooksWeb.Models;

namespace zBooksWeb.Controllers;

public class CategoryController(ApplicationDbContext db) : Controller
{
    private readonly ApplicationDbContext _db = db;

    public IActionResult Index()
    {
        // Update the category list
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        // Return the view when receiving an invalid model
        if (!ModelState.IsValid) return View(obj);
        // Add the valid model, save the db, and then re-generate the category list
        _db.Categories.Add(obj);
        _db.SaveChanges(); 
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id is null or 0) return NotFound();
        var categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb is null) return NotFound();
        return View(categoryFromDb);
    }
    
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (!ModelState.IsValid) return View(obj);
        _db.Categories.Update(obj);
        _db.SaveChanges(); 
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0) return NotFound();
        var categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb is null) return NotFound();
        return View(categoryFromDb);
    }
    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.Categories.Find(id);
        if (obj is null) return NotFound();
        if (!ModelState.IsValid) return View(obj);
        _db.Categories.Remove(obj);
        _db.SaveChanges(); 
        return RedirectToAction("Index");
    }
}