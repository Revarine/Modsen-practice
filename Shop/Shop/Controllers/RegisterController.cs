using Microsoft.AspNetCore.Mvc;
using Shop.Exceptions;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _registerService;
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(IRegisterService registerService, ILogger<RegisterController> logger)
    {
        _registerService = registerService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userDto = await _registerService.RegisterUserAsync(registerDto, cancellationToken);
            return CreatedAtAction(nameof(RegisterUserAsync), userDto);
        }
        catch (RepeatingNameException ex)
        {
            _logger.LogWarning(ex, "Repeating name exception occurred during user registration");
            return Conflict(ex.Message);
        }
        catch (UserAlreadyExistsException ex)
        {
            _logger.LogWarning(ex, "User already exists exception occurred during user registration");
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during user registration");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering the user");
        }
    }
}