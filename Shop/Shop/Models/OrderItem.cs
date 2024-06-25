using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}