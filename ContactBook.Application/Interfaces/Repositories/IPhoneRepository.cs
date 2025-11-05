using ContactBook.Domain.Entities;

namespace ContactBook.Application.Interfaces.Repositories;

public interface IPhoneRepository : IGenericRepository<Phone>
{
    Task<IEnumerable<Phone>> GetPhonesByUserIdAsync(int userId, CancellationToken cancellationToken);
}
