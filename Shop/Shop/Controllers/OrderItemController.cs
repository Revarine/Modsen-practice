using Microsoft.AspNetCore.Mvc;
using Shop.Exceptions;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItemById(int id, CancellationToken cancellationToken)
    {
        var orderItem = await _orderItemService.GetOrderItemByIdAsync(id, cancellationToken);
        return Ok(orderItem);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAllOrderItems(
        CancellationToken cancellationToken = default)
    {
        var orderItems = await _orderItemService.GetAllOrderItemsAsync(cancellationToken);
        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemDto>> CreateOrderItem(OrderItemDto newOrderItem,
        CancellationToken cancellationToken = default)
    {
        var createdOrderItem = await _orderItemService.CreateOrderItemAsync(newOrderItem, cancellationToken);
        return CreatedAtAction(nameof(CreateOrderItem), new { id = createdOrderItem.Id }, createdOrderItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderItemDto>> UpdateOrderItem(int id, OrderItemDto updatedOrderItem,
        CancellationToken cancellationToken = default)
    {
        await _orderItemService.UpdateOrderItemAsync(id, updatedOrderItem, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderItemDto>> DeleteOrderItem(int id, CancellationToken cancellationToken = default)
    {
        await _orderItemService.DeleteOrderItemAsync(id, cancellationToken);
        return NoContent();
    }
}