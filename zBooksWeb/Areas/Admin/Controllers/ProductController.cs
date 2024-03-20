using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;
using zBooks.Models.ViewModels;

namespace zBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    public IActionResult Index()
    {
        // Update the product list
        var objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
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
        var wwwRootPath = _webHostEnvironment.WebRootPath;
        if (file is not null)
        {
            char separator = Path.DirectorySeparatorChar;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var productPath = Path.Combine(wwwRootPath, @"images" + separator + "product");
            // If image already exists, delete it
            if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
            {
                var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart(separator));
                if (System.IO.File.Exists(oldImagePath)) System.IO.File.Delete(oldImagePath);
            }
            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            productVm.Product.ImageUrl = @separator + "images" + separator + "product" + separator + fileName;
            // Check if the view model exists (needs update) or needs added
            if (productVm.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVm.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVm.Product);
            }
        }
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