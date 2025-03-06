using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record CreateNoteDTO(
    [Required]
    [MinLength(3)]
    [MaxLength(512)]
    string Text,
    DateTime Deadline);