namespace ContactBook.Application.DTOs.User;

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}