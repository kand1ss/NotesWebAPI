using Application.Contracts;
using Application.DTO;
using Application.Exceptions;
using Application.Factories;
using Application.Mappers;
using Core.Contracts;
using Core.Models;

namespace Application.Services;

public class NoteService(INoteRepository noteRepository) : INoteService
{
    private async Task<IEnumerable<Note>> GetUserNotes(Guid id)
        => await noteRepository.GetByUserGuidAsync(id);
    
    private Note TryGetNoteById(int id, IEnumerable<Note> notes)
    {
        var note = notes.FirstOrDefault(n => n.Id == id);
        if (note is null)
            throw new NoteNotFoundException(id);
        
        return note;
    }


    public async Task CreateAsync(Guid userId, CreateNoteDTO noteData)
    {
        var note = NoteFactory.Create(noteData);
        note.AccountId = userId;
        
        await noteRepository.CreateAsync(note);
    }

    public async Task UpdateAsync(Guid userId, int id, UpdateNoteDTO noteData)
    {
        var userNotes = await GetUserNotes(userId);
        var note = TryGetNoteById(id, userNotes);
        
        note.Text = noteData.Text ?? note.Text;
        note.Deadline = noteData.Deadline ?? note.Deadline;
        
        await noteRepository.UpdateAsync(note);
    }

    public async Task DeleteAsync(Guid userId, int id)
    {
        var userNotes = await GetUserNotes(userId);
        var note = TryGetNoteById(id, userNotes);
        
        await noteRepository.DeleteAsync(note);
    }

    public async Task<NoteDTO> GetNoteByIdAsync(Guid userId, int id)
    {
        var userNotes = await GetUserNotes(userId);
        return TryGetNoteById(id, userNotes).ToDTO();
    }

    public async Task<IEnumerable<NoteDTO>> GetNotesByUserIdAsync(Guid id)
        => (await noteRepository.GetByUserGuidAsync(id)).ToDTOs();

    public async Task<IEnumerable<NoteDTO>> GetAllNotesAsync()
        => (await noteRepository.GetAllAsync()).ToDTOs();
}