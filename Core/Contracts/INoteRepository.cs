using Core.Models;

namespace Core.Contracts;

public interface INoteRepository
{
    public Task Create(Note note);
    public Task Update(Note note);
    public Task Delete(Note note);
    
    public Task<Note?> GetById(int id);
    public Task<IEnumerable<Note>> GetByUserGuid(Guid userGuid);
    
    public Task<IEnumerable<Note>> GetAll();
}