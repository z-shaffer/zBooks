using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public IActionResult Index()
    {
        // Update the product list
        var objProductList = _unitOfWork.Product.GetAll().ToList();
        return View(objProductList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Product obj)
    {
        // Return the view when receiving an invalid model
        if (!ModelState.IsValid) return View(obj);
        // Add the valid model, save the db, and then re-generate the product list
        _unitOfWork.Product.Add(obj);
        _unitOfWork.Save();
        TempData["success"] = "Success: Product created";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id is null or 0) return NotFound();
        var productFromDb = _unitOfWork.Product.Get(u=> u.Id == id);
        return View(productFromDb);
    }
    
    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (!ModelState.IsValid) return View(obj);
        _unitOfWork.Product.Update(obj);
        _unitOfWork.Save();
        TempData["success"] = "Success: Product updated";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0) return NotFound();
        var productFromDb = _unitOfWork.Product.Get(u=> u.Id == id);
        return View(productFromDb);
    }
    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.Product.Get(u=> u.Id == id);
        if (!ModelState.IsValid) return View(obj);
        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save(); 
        TempData["success"] = "Success: Product deleted";
        return RedirectToAction("Index");
    }
}