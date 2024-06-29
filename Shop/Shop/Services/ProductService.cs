using AutoMapper;
using Shop.Data.Interfaces;
using Shop.Models;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetItemAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetElementsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = (await _productRepository.GetElementsAsync())
                .Where(p => p.CategoryId == categoryId);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(int productId, ProductDto updatedProduct)
        {
            var product = _mapper.Map<Product>(updatedProduct);
            await _productRepository.UpdateAsync(productId, product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }
    }
}
