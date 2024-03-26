using System.ComponentModel.DataAnnotations;

namespace zBooks.Models;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int Quantity { get; set; } 
    [Required]
    public Product Product { get; set; }
}