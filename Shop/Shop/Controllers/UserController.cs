using BusinessLogic.Services.DTO;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(user);
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
        var createdUser = await _userService.CreateUserAsync(newUser, cancellationToken);
        return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UserDto updatedUser, CancellationToken cancellationToken = default)
    {
        await _userService.UpdateUserAsync(id, updatedUser, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUser(int id, CancellationToken cancellationToken = default)
    {
        await _userService.DeleteUserAsync(id, cancellationToken);
        return NoContent();
    }
}