using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.DTO;
using BusinessLogic.Services.Interfaces;
using DataAccess.Data.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services
{
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

            if (category is null)
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
            var categories = await _categoryRepository.GetElementsAsync(cancellationToken);
            if (categories.Any(x => x.Name == newCategory.Name && x.Id != newCategory.Id))
            {
                throw new RepeatingNameException("Names of several categories cannot be equals to each other");
            }

            var category = _mapper.Map<Category>(newCategory);
            await _categoryRepository.CreateAsync(category, cancellationToken);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(int categoryId, CategoryDto updatedCategory, CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetElementsAsync(cancellationToken);
            if (!categories.Any(x => x.Id == categoryId))
            {
                throw new NotFoundException("Cannot update non-existent category");
            }

            if (categories.Any(x => x.Name == updatedCategory.Name && x.Id != updatedCategory.Id))
            {
                throw new RepeatingNameException("Names of several categories cannot be equals to each other");
            }

            var category = _mapper.Map<Category>(updatedCategory);
            await _categoryRepository.UpdateAsync(categoryId, category, cancellationToken);
        }

        public async Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var existingCategory = await _categoryRepository.GetItemAsync(categoryId, cancellationToken);

            if (existingCategory is null)
            {
                throw new NotFoundException("Cannot delete non-existent category");
            }

            await _categoryRepository.DeleteAsync(categoryId, cancellationToken);
        }
    }
}
