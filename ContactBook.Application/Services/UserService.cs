using System.Threading.Tasks;
using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Application.Interfaces.Services;

namespace ContactBook.Application.Services;

internal class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Domain.Entities.User?> GetUserByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await userRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await userRepository.GetAllAsync(cancellationToken);
    }

    public async Task AddUserAsync(Domain.Entities.User user, CancellationToken cancellationToken)
    {
        await userRepository.AddAsync(user, cancellationToken);
    }

    public async Task UpdateUser(Domain.Entities.User user , CancellationToken cancellationToken)
    {
        await userRepository.UpdateAsync(user , cancellationToken);
    }

    public async Task DeleteUser(Domain.Entities.User user, CancellationToken cancellationToken)
    {
        await userRepository.DeleteAsync(user, cancellationToken);
    }
}