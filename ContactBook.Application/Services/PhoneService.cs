using ContactBook.Application.DTOs.Phone;
using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Application.Interfaces.Services;
using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;

namespace ContactBook.Application.Services;

internal class PhoneService : IPhoneService
{
    private readonly IPhoneRepository _phoneRepository;
    private readonly IUserRepository _userRepository;

    public PhoneService(IPhoneRepository phoneRepository,
        IUserRepository userRepository)
    {
        _phoneRepository = phoneRepository;
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<PhoneDto?>> GetPhoneByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            return ServiceResult<PhoneDto?>.FailResult("User not found", ErrorCode.NotFound);

        var phone = await _phoneRepository.GetByIdAsync(id, cancellationToken);
        if (phone is null)
            return ServiceResult<PhoneDto?>.FailResult("Phone not found", ErrorCode.NotFound);

        return ServiceResult<PhoneDto?>.SuccessResult(new PhoneDto
        {
            Id = phone.Id,
            PhoneNumber = phone.PhoneNumber,
            UserId = phone.UserId
        });
    }

    public async Task<ServiceResult<IEnumerable<PhoneDto>>> GetAllPhonesAsync(CancellationToken cancellationToken)
    {
        var phones = await _phoneRepository.GetAllAsync(cancellationToken);
        var dtos = phones.Select(p => new PhoneDto
        {
            Id = p.Id,
            PhoneNumber = p.PhoneNumber,
            UserId = p.UserId
        });

        return ServiceResult<IEnumerable<PhoneDto>>.SuccessResult(dtos);
    }

    public async Task<ServiceResult<PhoneDto>> AddPhoneAsync(CreatePhoneDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId, cancellationToken);
        if (user is null)
            return ServiceResult<PhoneDto>.FailResult("User not found", ErrorCode.NotFound);

        var phone = new Phone
        {
            PhoneNumber = dto.PhoneNumber,
            UserId = dto.UserId
        };

        await _phoneRepository.AddAsync(phone, cancellationToken);

        return ServiceResult<PhoneDto>.SuccessResult(new PhoneDto
        {
            Id = phone.Id,
            PhoneNumber = phone.PhoneNumber,
            UserId = phone.UserId
        });
    }

    public async Task<ServiceResult> UpdatePhoneAsync(int id, UpdatePhoneDto dto, CancellationToken cancellationToken)
    {
        var phone = await _phoneRepository.GetByIdAsync(id, cancellationToken);
        if (phone is null)
            return ServiceResult.FailResult("Phone not found", ErrorCode.NotFound);

        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            phone.PhoneNumber = dto.PhoneNumber;

        if (dto.UserId.HasValue)
            phone.UserId = dto.UserId.Value;

        await _phoneRepository.UpdateAsync(phone, cancellationToken);

        return ServiceResult.SuccessResult("Phone updated successfully");
    }

    public async Task<ServiceResult> DeletePhoneAsync(int id, CancellationToken cancellationToken)
    {
        var phone = await _phoneRepository.GetByIdAsync(id, cancellationToken);
        if (phone == null)
            return ServiceResult.FailResult("Phone not found", ErrorCode.NotFound);

        await _phoneRepository.DeleteAsync(phone, cancellationToken);
        return ServiceResult.SuccessResult("Phone deleted successfully");
    }
}
