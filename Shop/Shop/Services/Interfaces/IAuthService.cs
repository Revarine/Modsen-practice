
using Shop.Services.DTO;

namespace Shop.Services.Interfaces;
public interface IAuthService
{
    Task<string> AuthenticateAsync( LoginDto loginDto, CancellationToken cancellationToken = default );
}
