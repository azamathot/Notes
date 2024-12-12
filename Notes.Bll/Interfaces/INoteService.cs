using Notes.Common.DTOs;

namespace Notes.Bll.Interfaces
{
    public interface INoteService
    {
        Task<NoteDto?> GetNoteByIdAsync(int id);
        Task<IEnumerable<NoteDto>> GetAllNotesByUserIdAsync(Guid userId);
        Task<IEnumerable<NoteDto>> GetSharedNotesByUserIdAsync(Guid userId);
        Task<NoteDto2> AddNoteAsync(NoteDto2 noteDto);
        Task DeleteNoteAsync(int id);
        Task<ResponceDto<NoteDto2>> UpdateNoteAsync(NoteDto2 note, Guid userId);
        Task UpdateNoteSharesAsync(int noteId, List<ShareItemDto> noteShares);
        Task<bool> NoteExistAsync(int id);
    }
}
