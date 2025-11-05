using ContactBook.Application.DTOs.User;
using ContactBook.Domain.Common;

namespace ContactBook.Application.Interfaces.Services;

public interface IUserService
{
    Task<ServiceResult<UserDto?>> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<ServiceResult<UserDto>> AddUserAsync(CreateUserDto dto, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken);
    Task<ServiceResult> DeleteUserAsync(int id, CancellationToken cancellationToken);
}