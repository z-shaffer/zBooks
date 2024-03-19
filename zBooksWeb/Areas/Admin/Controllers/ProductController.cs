using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;
using zBooks.Models.ViewModels;

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
    public IActionResult Upsert(int? id)
    {
        ProductVM productVm = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Product = new Product()
        };
        // Handles creation
        if (id is null or 0) return View(productVm);
        // Handles updating
        productVm.Product = _unitOfWork.Product.Get(u => u.Id == id);
        return View(productVm);
    }
    
    [HttpPost]
    public IActionResult Upsert(ProductVM productVm, IFormFile? file)
    {
        // Return the view when receiving an invalid model
        if (!ModelState.IsValid)
        {
            productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVm);
        }
        // Add the valid model, save the db, and then re-generate the product list
        _unitOfWork.Product.Add(productVm.Product);
        _unitOfWork.Save();
        TempData["success"] = "Success: Product created";
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