using Shop.Services.DTO;

namespace Shop.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
    Task<CategoryDto> CreateCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken = default);
    Task UpdateCategoryAsync(int productId, CategoryDto updatedCategory, CancellationToken cancellationToken = default);
    Task DeleteCategoryAsync(int productId, CancellationToken cancellationToken = default);
}