using Microsoft.AspNetCore.Mvc;
using Shop.Exceptions;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);
            return Ok(user);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto newUser, CancellationToken cancellationToken = default)
    {
        try
        {
            var createdUser = await _userService.CreateUserAsync(newUser, cancellationToken);
            return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Id}, createdUser);
        }
        catch (RepeatingNameException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UserDto updatedUser, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.UpdateUserAsync(id, updatedUser, cancellationToken);
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
    
    
}