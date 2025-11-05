using AutoMapper;
using ContactBook.Shared.DTOs.Phone;
using ContactBook.Application.Interfaces.Repositories;
using ContactBook.Application.Interfaces.Services;
using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace ContactBook.Application.Services;

internal class PhoneService : IPhoneService
{
    private readonly IPhoneRepository _phoneRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<PhoneService> _logger;
    private readonly IMapper _mapper;

    public PhoneService(
        IPhoneRepository phoneRepository,
        IUserRepository userRepository,
        ILogger<PhoneService> logger,
        IMapper mapper)
    {
        _phoneRepository = phoneRepository;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PhoneDto?>> GetPhoneByIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting phone by ID: {PhoneId}", id);

        var phone = await _phoneRepository.GetByIdAsync(id, cancellationToken);
        if (phone is null)
        {
            _logger.LogWarning("Phone not found: {PhoneId}", id);
            return ServiceResult<PhoneDto?>.FailResult("Phone not found", ErrorCode.NotFound);
        }

        _logger.LogInformation("Phone found. ID: {PhoneId}, UserId: {UserId}", phone.Id, phone.UserId);

        return ServiceResult<PhoneDto?>.SuccessResult(new PhoneDto
        {
            Id = phone.Id,
            PhoneNumber = phone.PhoneNumber,
            UserId = phone.UserId
        });
    }

    public async Task<ServiceResult<IEnumerable<PhoneDto>>> GetAllPhonesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all phones");

        var phones = await _phoneRepository.GetAllAsync(cancellationToken);
        
        var dtos = _mapper.Map<IEnumerable<PhoneDto>>(phones);

        _logger.LogInformation("Loaded {Count} phones", dtos.Count());

        return ServiceResult<IEnumerable<PhoneDto>>.SuccessResult(dtos);
    }

    public async Task<ServiceResult<PhoneDto>> AddPhoneAsync(CreatePhoneDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding phone {@PhoneDto}", dto);

        var user = await _userRepository.GetByIdAsync(dto.UserId, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("Cannot add phone — User not found: {UserId}", dto.UserId);
            return ServiceResult<PhoneDto>.FailResult("User not found", ErrorCode.NotFound);
        }

        var phone = _mapper.Map<Phone>(dto);

        await _phoneRepository.AddAsync(phone, cancellationToken);

        _logger.LogInformation("Phone created. Id: {PhoneId}, UserId: {UserId}", phone.Id, phone.UserId);

        return ServiceResult<PhoneDto>.SuccessResult(new PhoneDto
        {
            Id = phone.Id,
            PhoneNumber = phone.PhoneNumber,
            UserId = phone.UserId
        });
    }

    public async Task<ServiceResult> UpdatePhoneAsync(int id, UpdatePhoneDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating phone {PhoneId} with {@UpdateDto}", id, dto);

        var phone = await _phoneRepository.GetByIdAsync(id, cancellationToken);
        if (phone is null)
        {
            _logger.LogWarning("Phone not found for update: {PhoneId}", id);
            return ServiceResult.FailResult("Phone not found", ErrorCode.NotFound);
        }

        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            phone.PhoneNumber = dto.PhoneNumber;

        if (dto.UserId.HasValue)
        {
            var userExists = await _userRepository.GetByIdAsync(dto.UserId.Value, cancellationToken);
            if (userExists is null)
            {
                _logger.LogWarning("Cannot reassign phone {PhoneId} — user not found {UserId}", id, dto.UserId.Value);
                return ServiceResult.FailResult("Target user not found", ErrorCode.NotFound);
            }
            phone.UserId = dto.UserId.Value;
        }

        await _phoneRepository.UpdateAsync(phone, cancellationToken);

        _logger.LogInformation("Phone updated successfully {PhoneId}", id);

        return ServiceResult.SuccessResult("Phone updated successfully");
    }

    public async Task<ServiceResult> DeletePhoneAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting phone {PhoneId}", id);

        var phone = await _phoneRepository.GetByIdAsync(id, cancellationToken);
        if (phone is null)
        {
            _logger.LogWarning("Phone not found for delete: {PhoneId}", id);
            return ServiceResult.FailResult("Phone not found", ErrorCode.NotFound);
        }

        await _phoneRepository.DeleteAsync(phone, cancellationToken);

        _logger.LogInformation("Phone deleted successfully {PhoneId}", id);

        return ServiceResult.SuccessResult("Phone deleted successfully");
    }

    public async Task<ServiceResult<IEnumerable<PhoneDto>>> GetPhonesByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting phones for UserId: {UserId}", userId);

        var phones = await _phoneRepository.GetPhonesByUserIdAsync(userId, cancellationToken);
        
        var dtos = _mapper.Map<IEnumerable<PhoneDto>>(phones);

        _logger.LogInformation("Loaded {Count} phones for UserId: {UserId}", dtos.Count(), userId);

        return ServiceResult<IEnumerable<PhoneDto>>.SuccessResult(dtos);
    }
}
