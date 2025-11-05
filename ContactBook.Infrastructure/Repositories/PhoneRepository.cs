using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Repositories;

internal class PhoneRepository(AppDbContext context) : GenericRepository<Phone>(context), IPhoneRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Phone>> GetPhonesByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Phones
            .Where(p => p.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}