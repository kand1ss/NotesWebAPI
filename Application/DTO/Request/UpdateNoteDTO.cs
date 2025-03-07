using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record UpdateNoteDTO(
    [MinLength(3)]
    [MaxLength(512)]
    string? Text,
    DateTime? Deadline
    );