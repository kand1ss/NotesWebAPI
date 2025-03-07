using Application.DTO;
using Core.Models;

namespace Application.Mappers;

public static class NoteMapper
{
    public static NoteDTO ToDTO(this Note note)
        => new(note.Text, note.Deadline);
    
    public static IEnumerable<NoteDTO> ToDTOs(this IEnumerable<Note> notes)
        => notes.Select(ToDTO);
}