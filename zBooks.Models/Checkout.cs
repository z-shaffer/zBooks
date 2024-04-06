using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace zBooks.Models;

public class Checkout
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(30, ErrorMessage = "Full Name must be less than {1} characters.")]
    [DisplayName("Full Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email Address is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [StringLength(30, ErrorMessage = "Email Address must be less than {1} characters.")]
    [DisplayName("Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Street Address is required.")]
    [StringLength(30, ErrorMessage = "Street Address must be less than {1} characters.")]
    [DisplayName("Street Address")]
    public string Address { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [StringLength(30, ErrorMessage = "City must be less than {1} characters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters.")]
    public string State { get; set; }

    [Required(ErrorMessage = "Zip Code is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip Code must be 5 characters.")]
    public string Zip { get; set; }

    [Required(ErrorMessage = "Credit Card Number is required.")]
    [CreditCard(ErrorMessage = "Invalid Credit Card Number.")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "Credit Card Number must be 16 characters.")]
    [DisplayName("Credit Card Number")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "Expiration Date is required.")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "Expiration Date must be 4 characters.")]
    [DisplayName("Expiration Date (MMYY)")]
    public string Expiration { get; set; }

    [Required(ErrorMessage = "CVV is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 characters.")]
    public string Cvv { get; set; }
}