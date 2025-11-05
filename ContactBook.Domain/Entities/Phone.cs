using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBook.Domain.Entities;

public class Phone
{
    [Key]
    public int Id { get; private set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
    
    [ForeignKey("User")]
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}