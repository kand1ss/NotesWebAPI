using Core.Contracts;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NoteRepository(DataContext context) : INoteRepository
{
    public async Task CreateAsync(Note note)
    {
        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Note note)
    {
        context.Notes.Update(note);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Note note)
    {
        context.Notes.Remove(note);
        await context.SaveChangesAsync();
    }

    public async Task<Note?> GetByIdAsync(int id)
        => await context.Notes.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Note>> GetByUserGuidAsync(Guid userGuid)
        => await context.Notes.Where(x => x.AccountId == userGuid).ToListAsync();

    public async Task<IEnumerable<Note>> GetAllAsync()
        => await context.Notes.ToListAsync();
}