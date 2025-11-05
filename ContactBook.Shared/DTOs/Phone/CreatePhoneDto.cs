namespace ContactBook.Shared.DTOs.Phone;

public class CreatePhoneDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
}