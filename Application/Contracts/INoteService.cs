using Application.DTO;

namespace Application.Contracts;

public interface INoteService
{
    Task CreateAsync(Guid userId, CreateNoteDTO noteData);
    Task UpdateAsync(Guid userId, int id, UpdateNoteDTO note);
    Task DeleteAsync(Guid userId, int id);
    
    Task<NoteDTO> GetNoteByIdAsync(Guid userId, int id);
    Task<IEnumerable<NoteDTO>> GetNotesByUserIdAsync(Guid id);
    Task<IEnumerable<NoteDTO>> GetAllNotesAsync();
}