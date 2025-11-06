using System.Net.Http.Json;
using ContactBook.Shared.DTOs.User;

namespace ContactBook.Client.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserDto>>("users") ?? [];
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<UserDto>($"users/{id}") ??
            throw new Exception("User not found");
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var response = await _httpClient.PostAsJsonAsync("users", createUserDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
            throw new Exception("Failed to create user");
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"users/{id}", updateUserDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
            throw new Exception("Failed to update user");
    }

    public async Task DeleteUserAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"users/{id}");
        response.EnsureSuccessStatusCode();
    }
}