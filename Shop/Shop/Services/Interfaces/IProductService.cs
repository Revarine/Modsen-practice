using Shop.Services.DTO;

namespace Shop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDto> CreateProductAsync(ProductDto newProduct);
        Task UpdateProductAsync(int productId, ProductDto updatedProduct);
        Task DeleteProductAsync(int productId);
    }
}
