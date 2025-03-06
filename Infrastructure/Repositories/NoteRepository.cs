using Core.Contracts;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NoteRepository(DataContext context) : INoteRepository
{
    public async Task Create(Note note)
    {
        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();
    }

    public async Task Update(Note note)
    {
        context.Notes.Update(note);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Note note)
    {
        context.Notes.Remove(note);
        await context.SaveChangesAsync();
    }

    public async Task<Note?> GetById(int id)
        => await context.Notes.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Note>> GetByUserGuid(Guid userGuid)
        => await context.Notes.Where(x => x.AccountId == userGuid).ToListAsync();

    public async Task<IEnumerable<Note>> GetAll()
        => await context.Notes.ToListAsync();
}