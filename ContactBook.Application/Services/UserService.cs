using ContactBook.Application.DTOs.User;
using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Application.Interfaces.Services;
using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;

namespace ContactBook.Application.Services;

internal class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<UserDto?>> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user == null)
            return ServiceResult<UserDto?>.FailResult("User not found", ErrorCode.NotFound);

        var dto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };

        return ServiceResult<UserDto?>.SuccessResult(dto);
    }

    public async Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var dtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            DateOfBirth = u.DateOfBirth
        });

        return ServiceResult<IEnumerable<UserDto>>.SuccessResult(dtos);
    }

    public async Task<ServiceResult<UserDto>> AddUserAsync(CreateUserDto dto, CancellationToken cancellationToken)
    {
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

        return ServiceResult<UserDto>.SuccessResult(resultDto);
    }

    public async Task<ServiceResult> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (user is null)
            return ServiceResult.FailResult("User not found", ErrorCode.NotFound);

        if (!string.IsNullOrEmpty(dto.Name))
            user.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Email))
            user.Email = dto.Email;

        if (dto.DateOfBirth.HasValue)
            user.DateOfBirth = dto.DateOfBirth.Value;

        await _userRepository.UpdateAsync(user, cancellationToken);

        return ServiceResult.SuccessResult("User updated successfully");
    }

    public async Task<ServiceResult> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            return ServiceResult.FailResult("User not found", ErrorCode.NotFound);

        await _userRepository.DeleteAsync(user, cancellationToken);
        return ServiceResult.SuccessResult("User deleted successfully");
    }
}
