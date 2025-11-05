using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Data;

namespace ContactBook.Infrastructure.Repositories;

internal class PhoneRepository(AppDbContext context) : GenericRepository<Phone>(context), IPhoneRepository
{}