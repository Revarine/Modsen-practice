using Microsoft.AspNetCore.Mvc;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _registerService;

    public RegisterController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto, CancellationToken cancellationToken = default)
    {
       
        var userDto = await _registerService.RegisterUserAsync(registerDto, cancellationToken);
        return CreatedAtAction(nameof(RegisterUser), userDto);
       
    }
}