using AutoMapper;
using Shop.Data.Interfaces;
using Shop.Exceptions;
using Shop.Models;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = _categoryRepository;
        _mapper = _mapper;
    }
    
    public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        var category = _categoryRepository.GetItemAsync(categoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException($"Category with ID {categoryId} not found");
        }

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = _categoryRepository.GetElementsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken = default)
    {
        var category = _mapper.Map<Category>(newCategory);
        await _categoryRepository.CreateAsync(category, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task UpdateCategoryAsync(int categoryId, CategoryDto updatedCategory, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _categoryRepository.GetItemAsync(categoryId, cancellationToken);
        
        if (existingCategory == null)
        {
            throw new NotFoundException("Cannot update non-existent category");
        }
        
        var category = _mapper.Map<Category>(updatedCategory);
        await _categoryRepository.UpdateAsync(categoryId, category, cancellationToken);
    }

    public async Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        var existingCategory = await _categoryRepository.GetItemAsync(categoryId, cancellationToken);
        
        if (existingCategory == null)
        {
            throw new NotFoundException("Cannot delete non-existent category");
        }
        
        await _categoryRepository.DeleteAsync(categoryId, cancellationToken);
    }
}