using AutoMapper;
using Shop.Data.Interfaces;
using Shop.Exceptions;
using Shop.Models;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetItemAsync(userId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException($"User with ID {userId} not found.");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetElementsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> CreateUserAsync(UserDto newUser, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(newUser);
        await _userRepository.CreateAsync(user, cancellationToken);
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateUserAsync(int userId, UserDto updatedUser, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(updatedUser);
        await _userRepository.UpdateAsync(userId, user, cancellationToken);
    }

    public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        await _userRepository.DeleteAsync(userId, cancellationToken);
    }
}