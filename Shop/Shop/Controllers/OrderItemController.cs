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
        try
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id, cancellationToken);
            return Ok(orderItem);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAllOrderItems(CancellationToken cancellationToken = default)
    {
        var orderItems = await _orderItemService.GetAllOrderItemsAsync(cancellationToken);
        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemDto>> CreateOrderItem(OrderItemDto newOrderItem, CancellationToken cancellationToken = default)
    {
        try
        {
            var createdOrderItem = await _orderItemService.CreateOrderItemAsync(newOrderItem, cancellationToken);
            return CreatedAtAction(nameof(CreateOrderItem), new { id = createdOrderItem.Id}, createdOrderItem);
        }
        catch (RepeatingNameException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderItemDto>> UpdateOrderItem(int id, OrderItemDto updatedOrderItem, CancellationToken cancellationToken = default)
    {
        try
        {
            await _orderItemService.UpdateOrderItemAsync(id, updatedOrderItem, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RepeatingNameException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderItemDto>> DeleteOrderItem(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _orderItemService.DeleteOrderItemAsync(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}