using AutoMapper;
using Shop.Data.Interfaces;
using Shop.Data.Repositories;
using Shop.Exceptions;
using Shop.Models;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetItemAsync(productId, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetElementsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var category = (await _categoryRepository.GetElementsAsync(cancellationToken))
                .FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                throw new NotFoundException($"Category with ID {categoryId} not found.");
            }

            var products = (await _productRepository.GetElementsAsync(cancellationToken))
                .Where(p => p.CategoryId == categoryId);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto newProduct, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Product>(newProduct);
            await _productRepository.CreateAsync(product, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(int productId, ProductDto updatedProduct, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Product>(updatedProduct);
            await _productRepository.UpdateAsync(productId, product, cancellationToken);
        }

        public async Task DeleteProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            await _productRepository.DeleteAsync(productId, cancellationToken);
        }
    }
}
