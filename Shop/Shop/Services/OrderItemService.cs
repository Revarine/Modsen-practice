using AutoMapper;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using Shop.Exceptions;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IRepository<OrderItem> orderItemRepository, IRepository<Order> orderRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemRepository.GetItemAsync(orderItemId, cancellationToken);
            if (orderItem is null)
            {
                throw new NotFoundException($"OrderItem with ID {orderItemId} not found.");
            }
            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync(CancellationToken cancellationToken = default)
        {
            var orderItems = await _orderItemRepository.GetElementsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        }

        public async Task<OrderItemDto> CreateOrderItemAsync(OrderItemDto newOrderItem, CancellationToken cancellationToken = default)
        {
            var orderItem = _mapper.Map<OrderItem>(newOrderItem);
            await _orderItemRepository.CreateAsync(orderItem, cancellationToken);
            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task UpdateOrderItemAsync(int orderItemId, OrderItemDto updatedOrderItem, CancellationToken cancellationToken = default)
        {
            var orderItem = _mapper.Map<OrderItem>(updatedOrderItem);
            await _orderItemRepository.UpdateAsync(orderItemId, orderItem, cancellationToken);
        }

        public async Task DeleteOrderItemAsync(int orderItemId, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemRepository.GetItemAsync(orderItemId, cancellationToken);

            if (orderItem is null)
            {
                throw new NotFoundException($"OrderItem with ID {orderItemId} not found.");
            }

            await _orderItemRepository.DeleteAsync(orderItemId, cancellationToken);
        }
    }
}
