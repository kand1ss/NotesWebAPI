using Core.Models;

namespace Core.Contracts;

public interface INoteRepository
{
    public Task CreateAsync(Note note);
    public Task UpdateAsync(Note note);
    public Task DeleteAsync(Note note);
    
    public Task<Note?> GetByIdAsync(int id);
    public Task<IEnumerable<Note>> GetByUserGuidAsync(Guid userGuid);
    
    public Task<IEnumerable<Note>> GetAllAsync();
}