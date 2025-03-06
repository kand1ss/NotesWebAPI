using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record LoginAccountDTO(
    [Required]
    string Login,
    [Required]
    string Password);