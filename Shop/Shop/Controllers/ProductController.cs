using Microsoft.AspNetCore.Mvc;
using Shop.Exceptions;
using Shop.Services.DTO;
using Shop.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController( IProductService productService )
    {
        _productService = productService;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts( CancellationToken cancellationToken = default )
    {
        var products = await _productService.GetAllProductsAsync(cancellationToken);
        return Ok(products);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById( int id, CancellationToken cancellationToken = default )
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);
            return Ok(product);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // GET: api/Products/Category/5
    [HttpGet("Category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory( int categoryId, CancellationToken cancellationToken = default )
    {
        try
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId, cancellationToken);
            return Ok(products);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct( ProductDto newProduct, CancellationToken cancellationToken = default )
    {
        try
        {
            var createdProduct = await _productService.CreateProductAsync(newProduct, cancellationToken);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct( int id, ProductDto updatedProduct, CancellationToken cancellationToken = default )
    {
        try
        {
            await _productService.UpdateProductAsync(id, updatedProduct, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct( int id, CancellationToken cancellationToken = default )
    {
        try
        {
            await _productService.DeleteProductAsync(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
