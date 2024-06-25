using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models;

public class Product
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    [Required]
    [Range(0.00, 999999.99)]
    public decimal Price { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}