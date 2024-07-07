using BusinessLogic.Services.DTO;

namespace BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        Task<OrderDto> CreateOrderAsync(OrderDto newOrder, CancellationToken cancellationToken = default);
        Task UpdateOrderAsync(int orderId, OrderDto updatedOrder, CancellationToken cancellationToken = default);
        Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken = default);
    }
}
