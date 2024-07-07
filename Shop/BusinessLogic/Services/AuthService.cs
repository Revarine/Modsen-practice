using AutoMapper;
using BusinessLogic.Services.DTO;
using BusinessLogic.Services.Interfaces;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
        {
            var user = (await _userRepository.GetElementsAsync(cancellationToken))
                            .FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
