using BusinessLogic.Services.DTO;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetAllProductsAsync(cancellationToken);
        return Ok(products);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken cancellationToken = default)
    {
        var product = await _productService.GetProductByIdAsync(id, cancellationToken);
        return Ok(product);
    }

    // GET: api/Products/Category/5
    [HttpGet("Category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId, cancellationToken);
        return Ok(products);

    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto newProduct, CancellationToken cancellationToken = default)
    {
        var createdProduct = await _productService.CreateProductAsync(newProduct, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto updatedProduct, CancellationToken cancellationToken = default)
    {
        await _productService.UpdateProductAsync(id, updatedProduct, cancellationToken);
        return NoContent();
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken = default)
    {
        await _productService.DeleteProductAsync(id, cancellationToken);
        return NoContent();

    }
}
