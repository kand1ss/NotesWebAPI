using Application.DTO;

namespace Application.Contracts;

public interface INoteService
{
    Task CreateAsync(NoteDTO note);
    Task UpdateAsync(NoteDTO note);
    Task DeleteAsync(int id);
    
    Task<NoteDTO> GetNoteByIdAsync(int id);
    Task<IEnumerable<NoteDTO>> GetNotesByUserIdAsync(Guid id);
    Task<IEnumerable<NoteDTO>> GetAllNotesAsync();
}