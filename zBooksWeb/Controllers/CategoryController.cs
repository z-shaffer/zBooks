using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooksWeb.Controllers;

public class CategoryController(ICategoryRepository db) : Controller
{
    private readonly ICategoryRepository _categoryRepo = db;
    
    public IActionResult Index()
    {
        // Update the category list
        var objCategoryList = _categoryRepo.GetAll().ToList();
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
        _categoryRepo.Add(obj);
        _categoryRepo.Save();
        TempData["success"] = "Success: Category created";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id is null or 0) return NotFound();
        var categoryFromDb = _categoryRepo.Get(u=> u.Id == id);
        return View(categoryFromDb);
    }
    
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (!ModelState.IsValid) return View(obj);
        _categoryRepo.Update(obj);
        _categoryRepo.Save();
        TempData["success"] = "Success: Category updated";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0) return NotFound();
        var categoryFromDb = _categoryRepo.Get(u=> u.Id == id);
        return View(categoryFromDb);
    }
    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var obj = _categoryRepo.Get(u=> u.Id == id);
        if (!ModelState.IsValid) return View(obj);
        _categoryRepo.Remove(obj);
        _categoryRepo.Save(); 
        TempData["success"] = "Success: Category deleted";
        return RedirectToAction("Index");
    }
}