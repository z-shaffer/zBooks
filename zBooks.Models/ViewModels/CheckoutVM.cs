using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace zBooks.Models.ViewModels;

public class CheckoutVM
{
    public Checkout Checkout { get; set; }
}