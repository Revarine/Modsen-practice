using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    public virtual ICollection<Product> Products { get; set; }
}