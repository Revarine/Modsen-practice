using AutoMapper;
using Shop.Data.Interfaces;
using Shop.Exceptions;
using Shop.Models;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Order> _userRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetItemAsync(orderId, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetElementsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {

            var user = (await _userRepository.GetElementsAsync(cancellationToken))
                .FirstOrDefault(c => c.Id == userId);
            if (user is null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            var orders = (await _orderRepository.GetElementsAsync(cancellationToken))
                .Where(o => o.UserId == userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto newOrder, CancellationToken cancellationToken = default)
        {
            var order = _mapper.Map<Order>(newOrder);
            await _orderRepository.CreateAsync(order, cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task UpdateOrderAsync(int orderId, OrderDto updatedOrder, CancellationToken cancellationToken = default)
        {
            var existingOrder = await _orderRepository.GetItemAsync(orderId, cancellationToken);

            if (existingOrder is null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            var order = _mapper.Map<Order>(updatedOrder);
            await _orderRepository.UpdateAsync(orderId, order, cancellationToken);
        }

        public async Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetItemAsync(orderId, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            await _orderRepository.DeleteAsync(orderId, cancellationToken);
        }
    }
}
