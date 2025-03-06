using Application.DTO;
using Core.Models;

namespace Application.Mappers;

public class NoteMapper
{
    public static NoteDTO ToDTO(Note note)
        => new(note.Text, note.Deadline);
    
    public static IEnumerable<NoteDTO> ToDTOs(IEnumerable<Note> notes)
        => notes.Select(ToDTO);
}