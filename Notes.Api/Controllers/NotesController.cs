using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Bll.Interfaces;
using Notes.Common.DTOs;
using Notes.Dal.Entities;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        Guid currentUserId;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
            //Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);
        }

        // GET: api/Notes
        [HttpGet("GetMyNotes")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetMyNotes()
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);
            return Ok(await _noteService.GetAllNotesByUserIdAsync(currentUserId));
        }

        [HttpGet("GetSharedWithMeNotes")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetSharedWithMeNotes()
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);
            return Ok(await _noteService.GetSharedNotesByUserIdAsync(currentUserId));
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);
            var note = await _noteService.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }
            else if (!(note.UserId == currentUserId || (note.Shares != null && note.Shares.Any(x => x.UserId == currentUserId))))
            {
                return Forbid();
            }
            return Ok(note);
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutNote(NoteDto2 noteDto2)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);
            try
            {
                var result = await _noteService.UpdateNoteAsync(noteDto2, currentUserId);
                if (result.Status == Common.DTOs.StatusCode.Forbid)
                {
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                if (!(await NoteExists(noteDto2.Id.Value)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(NoteDto2 note)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);

            note.UserId = currentUserId;
            note.Id = null;
            note = await _noteService.AddNoteAsync(note);

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);

            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            else if (!(note.UserId == currentUserId || (note.Shares != null && note.Shares.Any(x => x.UserId == currentUserId) && note.Shares.FirstOrDefault(x => x.UserId == currentUserId)!.CanEdit)))
            {
                return Forbid();
            }

            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }

        private async Task<bool> NoteExists(int id)
        {
            return await _noteService.NoteExistAsync(id);
        }

        // POST: api/Notes/{id}/share
        [HttpPost("{noteId}/share")]
        public async Task<IActionResult> ShareNote(int noteId, [FromBody] List<ShareItemDto> noteShares)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out currentUserId);

            var note = await _noteService.GetNoteByIdAsync(noteId);

            if (note == null)
            {
                return NotFound();
            }
            else if (note.UserId != currentUserId) // Только владелец может расшаривать
                return Forbid();

            await _noteService.UpdateNoteSharesAsync(noteId, noteShares);
            return Ok();
        }

    }
}
