using Application.DTO;
using Core.Models;

namespace Application.Factories;

public static class NoteFactory
{
    public static Note Create(CreateNoteDTO note)
        => new()
        {
            Text = note.Text,
            Deadline = note.Deadline,
            CreatedUtc = DateTime.UtcNow
        };
}