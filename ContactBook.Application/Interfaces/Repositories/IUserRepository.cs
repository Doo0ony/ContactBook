using ContactBook.Domain.Entities;

namespace ContactBook.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<Domain.Entities.User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
