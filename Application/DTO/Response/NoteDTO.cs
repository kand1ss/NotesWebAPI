using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public record NoteDTO(
    string Text,
    DateTime Deadline);