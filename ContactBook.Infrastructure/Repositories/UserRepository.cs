using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Data;

namespace ContactBook.Infrastructure.Repositories;

internal class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{}