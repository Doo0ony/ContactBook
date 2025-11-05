namespace ContactBook.Application.Interfaces.Services;

public interface IUserService
{
    //TODO: Change to dtos
    Task<Domain.Entities.User?> GetUserByIdAsync(object id, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task AddUserAsync(Domain.Entities.User user, CancellationToken cancellationToken);
    void UpdateUser(Domain.Entities.User user);
    void DeleteUser(Domain.Entities.User user);
}