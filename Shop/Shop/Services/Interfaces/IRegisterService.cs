using Shop.Services.DTO;

namespace Shop.Services.Interfaces;

public interface IRegisterService
{
    Task<UserDto> RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken = default);
}
