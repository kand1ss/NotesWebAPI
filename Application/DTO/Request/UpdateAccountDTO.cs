using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record UpdateAccountDTO(
    [MinLength(4)]
    [MaxLength(20)]
    string? Login,
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\n")]
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