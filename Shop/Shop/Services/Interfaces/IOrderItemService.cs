using Shop.Services.DTO;

namespace Shop.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDto> GetOrderItemByIdAsync( int orderItemId, CancellationToken cancellationToken = default );
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync( CancellationToken cancellationToken = default );
        Task<OrderItemDto> CreateOrderItemAsync( OrderItemDto newOrderItem, CancellationToken cancellationToken = default );
        Task UpdateOrderItemAsync( int orderItemId, OrderItemDto updatedOrderItem, CancellationToken cancellationToken = default );
        Task DeleteOrderItemAsync( int orderItemId, CancellationToken cancellationToken = default );
    }
}
