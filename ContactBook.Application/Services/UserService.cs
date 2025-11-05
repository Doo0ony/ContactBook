using ContactBook.Application.DTOs.User;
using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Application.Interfaces.Services;
using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace ContactBook.Application.Services;

internal class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IGenericRepository<User> userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ServiceResult<UserDto?>> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting user by Id: {UserId}", id);

        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User with Id {UserId} not found", id);
            return ServiceResult<UserDto?>.FailResult("User not found", ErrorCode.NotFound);
        }

        var dto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };

        _logger.LogInformation("User with Id {UserId} retrieved successfully", id);
        return ServiceResult<UserDto?>.SuccessResult(dto);
    }

    public async Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all users");

        var users = await _userRepository.GetAllAsync(cancellationToken);
        var dtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            DateOfBirth = u.DateOfBirth
        });

        _logger.LogInformation("Retrieved {Count} users", dtos.Count());
        return ServiceResult<IEnumerable<UserDto>>.SuccessResult(dtos);
    }

    public async Task<ServiceResult<UserDto>> AddUserAsync(CreateUserDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding new user: {UserName}", dto.Name);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth
        };

        await _userRepository.AddAsync(user, cancellationToken);

        var resultDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };

        _logger.LogInformation("User {UserId} added successfully", user.Id);
        return ServiceResult<UserDto>.SuccessResult(resultDto);
    }

    public async Task<ServiceResult> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user with Id: {UserId}", dto.Id);

        var user = await _userRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("User with Id {UserId} not found", dto.Id);
            return ServiceResult.FailResult("User not found", ErrorCode.NotFound);
        }

        if (!string.IsNullOrEmpty(dto.Name))
            user.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Email))
            user.Email = dto.Email;

        if (dto.DateOfBirth.HasValue)
            user.DateOfBirth = dto.DateOfBirth.Value;

        await _userRepository.UpdateAsync(user, cancellationToken);

        _logger.LogInformation("User {UserId} updated successfully", user.Id);
        return ServiceResult.SuccessResult("User updated successfully");
    }

    public async Task<ServiceResult> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting user with Id: {UserId}", id);

        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("User with Id {UserId} not found", id);
            return ServiceResult.FailResult("User not found", ErrorCode.NotFound);
        }

        await _userRepository.DeleteAsync(user, cancellationToken);
        _logger.LogInformation("User {UserId} deleted successfully", id);
        return ServiceResult.SuccessResult("User deleted successfully");
    }
}
