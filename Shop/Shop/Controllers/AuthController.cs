using Microsoft.AspNetCore.Mvc;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController( IAuthService authService )
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login( [FromBody] LoginDto loginDto, CancellationToken cancellationToken = default )
    {
        try
        {
            var token = await _authService.AuthenticateAsync(loginDto, cancellationToken);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
