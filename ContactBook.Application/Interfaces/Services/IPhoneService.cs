using ContactBook.Application.DTOs.Phone;
using ContactBook.Domain.Common;

namespace ContactBook.Application.Interfaces.Services;

public interface IPhoneService
{
    Task<ServiceResult<PhoneDto?>> GetPhoneByIdAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult<IEnumerable<PhoneDto>>> GetAllPhonesAsync(CancellationToken cancellationToken);
    Task<ServiceResult<PhoneDto>> AddPhoneAsync(CreatePhoneDto dto, CancellationToken cancellationToken);
    Task<ServiceResult> UpdatePhoneAsync(int id, UpdatePhoneDto dto, CancellationToken cancellationToken);
    Task<ServiceResult> DeletePhoneAsync(int id, CancellationToken cancellationToken);
    Task<ServiceResult<IEnumerable<PhoneDto>>> GetPhonesByUserIdAsync(int userId, CancellationToken cancellationToken);
}
