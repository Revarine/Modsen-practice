﻿using BusinessLogic.Services.DTO;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<UserDto> RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken = default);
    }
}
