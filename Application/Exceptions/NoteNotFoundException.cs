namespace Application.Exceptions;

public class NoteNotFoundException : Exception
{
    public NoteNotFoundException(int id) : base($"Note with id \"{id}\" was not found.")
    {
    }
    public NoteNotFoundException(Guid id) : base($"Note by user id \"{id}\" was not found.")
    {
    }
}