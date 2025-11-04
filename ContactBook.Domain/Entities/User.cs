using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public ICollection<Phone> Phones { get; set; } = [];
}