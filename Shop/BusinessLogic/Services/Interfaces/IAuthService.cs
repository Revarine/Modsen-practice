using BusinessLogic.Services.DTO;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync( LoginDto loginDto, CancellationToken cancellationToken = default );
    }
}
