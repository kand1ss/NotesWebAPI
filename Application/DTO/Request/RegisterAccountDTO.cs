using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record RegisterAccountDTO(
    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    string Login,
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    string Email,
    [Required]
    [MinLength(5)]
    [MaxLength(32)]
    string Password,
    [MinLength(2)]
    [MaxLength(24)]
    string? Name,
    [MinLength(4)]
    [MaxLength(32)]
    string? LastName);