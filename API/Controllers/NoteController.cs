using System.Security.Claims;
using Application.Contracts;
using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/notes")]
public class NoteController(INoteService noteService) : ControllerBase
{
    private Guid GetUserGuid()
    {
        var userGuid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userGuid is null)
            throw new InvalidOperationException("User is not authenticated");
        return new Guid(userGuid);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNoteAsync([FromBody] CreateNoteDTO noteData)
    {
        var userGuid = GetUserGuid();
        await noteService.CreateAsync(userGuid, noteData);
        return Created();
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateNoteAsync(int id, [FromBody] UpdateNoteDTO noteData)
    {
        var userGuid = GetUserGuid();
        await noteService.UpdateAsync(userGuid, id, noteData);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteNoteAsync(int id)
    {
        var userGuid = GetUserGuid();
        await noteService.DeleteAsync(userGuid, id);
        return Ok();
    }

    [HttpGet("me/{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetNoteByIdAsync(int id)
    {
        var userGuid = GetUserGuid();
        var note = await noteService.GetNoteByIdAsync(userGuid, id);
        return Ok(note);
    }

    [HttpGet("me/all")]
    [Authorize]
    public async Task<IActionResult> GetNotesByUserIdAsync()
    {
        var userGuid = GetUserGuid();
        var notes = await noteService.GetNotesByUserIdAsync(userGuid);
        return Ok(notes);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllNotesAsync()
    {
        var notes = await noteService.GetAllNotesAsync();
        return Ok(notes);
    }
}