using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record UpdateAccountDTO(
    [MinLength(4)]
    [MaxLength(20)]
    string? Login,
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    string? Email,
    [MinLength(5)]
    [MaxLength(32)]
    string? Password,
    [MinLength(2)]
    [MaxLength(24)]
    string? Name,
    [MinLength(4)]
    [MaxLength(32)]
    string? LastName);