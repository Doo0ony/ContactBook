namespace ContactBook.Application.DTOs.Phone;

public class UpdatePhoneDto
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public int? UserId { get; set; }
}