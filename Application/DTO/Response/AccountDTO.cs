using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record AccountDTO(
    string Login,
    string Email,
    string Name,
    string LastName);