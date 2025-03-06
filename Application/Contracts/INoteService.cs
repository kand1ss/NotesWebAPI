using Application.DTO;

namespace Application.Contracts;

public interface INoteService
{
    Task Create(NoteDTO note);
    Task Update(NoteDTO note);
    Task Delete(int id);
    
    Task<NoteDTO> GetNoteById(int id);
    Task<IEnumerable<NoteDTO>> GetNotesByUserId(Guid id);
    Task<IEnumerable<NoteDTO>> GetAllNotes();
}