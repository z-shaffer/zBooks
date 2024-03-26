using System.ComponentModel.DataAnnotations;

namespace zBooks.Models;

public class ShoppingCart
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}