using Notes.Common.DTOs;
using Notes.Dal.Entities;

namespace Notes.Dal.Repositories
{
    public interface INoteRepository
    {
        Task<Note?> GetNoteByIdAsync(int id);
        Task<IEnumerable<Note>> GetAllNotesByUserIdAsync(Guid userId);
        Task<IEnumerable<Note>> GetSharedNotesByUserIdAsync(Guid userId);
        Task AddNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(int id);
        Task UpdateNoteSharesAsync(int noteId, List<ShareItemDto> noteShareDtos);
        Task<bool> NoteExistAsync(int id);
    }
}
