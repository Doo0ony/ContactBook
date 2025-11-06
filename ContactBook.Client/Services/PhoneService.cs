using System.Net.Http.Json;
using ContactBook.Shared.DTOs.Phone;
using Microsoft.Extensions.Logging;

namespace ContactBook.Client.Services;

public class PhoneService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PhoneService> _logger;

    public PhoneService(HttpClient httpClient, IConfiguration configuration, ILogger<PhoneService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<PhoneDto>> GetPhonesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching phones...");
            var response = await _httpClient.GetAsync("phones");

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Failed to load phones: {StatusCode} - {Message}", response.StatusCode, msg);
                return [];
            }

            var phones = await response.Content.ReadFromJsonAsync<List<PhoneDto>>();
            _logger.LogInformation("Loaded {Count} phones", phones?.Count ?? 0);

            return phones ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while loading phones");
            return [];
        }
    }

    public async Task<PhoneDto?> GetPhoneByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Fetching phone with id {Id}", id);
            var response = await _httpClient.GetAsync($"phones/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Phone {Id} not found: {Message}", id, msg);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<PhoneDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting phone by id {Id}", id);
            return null;
        }
    }

    public async Task<PhoneDto?> CreatePhoneAsync(CreatePhoneDto createPhoneDto)
    {
        try
        {
            _logger.LogInformation("Creating phone for user {UserId}", createPhoneDto.UserId);
            var response = await _httpClient.PostAsJsonAsync("phones", createPhoneDto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PhoneDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating phone");
            return null;
        }
    }

    public async Task<PhoneDto?> UpdatePhoneAsync(int id, UpdatePhoneDto updatePhoneDto)
    {
        try
        {
            _logger.LogInformation("Updating phone {Id}", id);
            var response = await _httpClient.PutAsJsonAsync($"phones/{id}", updatePhoneDto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PhoneDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating phone {Id}", id);
            return null;
        }
    }

    public async Task<bool> DeletePhoneAsync(int id)
    {
        try
        {
            _logger.LogInformation("Deleting phone {Id}", id);
            var response = await _httpClient.DeleteAsync($"phones/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Failed to delete phone {Id}: {Message}", id, msg);
                return false;
            }

            _logger.LogInformation("Phone {Id} deleted", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting phone {Id}", id);
            return false;
        }
    }

    public async Task<List<PhoneDto>> GetPhonesByUserIdAsync(int userId)
    {
        try
        {
            _logger.LogInformation("Fetching phones for user {UserId}", userId);
            var response = await _httpClient.GetAsync($"phones/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Failed to get phones for user {UserId}: {Message}", userId, msg);
                return [];
            }

            return await response.Content.ReadFromJsonAsync<List<PhoneDto>>() ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while loading phones by user {UserId}", userId);
            return [];
        }
    }
}
