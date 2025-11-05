using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Infrastructure.Repositories;

internal class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
