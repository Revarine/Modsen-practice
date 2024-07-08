using BusinessLogic.Services.DTO;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<ProductDto> CreateProductAsync(ProductDto newProduct, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(int productId, ProductDto updatedProduct, CancellationToken cancellationToken = default);
        Task DeleteProductAsync(int productId, CancellationToken cancellationToken = default);
    }
}
