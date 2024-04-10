using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zBooks.Models;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey("User")]
    public string CartId { get; set; }
    [Required]
    public int Quantity { get; set; } = 0;
    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
}