using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    public virtual User User { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}