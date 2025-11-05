namespace ContactBook.Application.DTOs.Phone;

public class PhoneDto
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
}