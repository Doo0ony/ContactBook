using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Repositories;

internal class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}