using Microsoft.EntityFrameworkCore;
using Notes.Common.DTOs;
using Notes.Dal.Entities;

namespace Notes.Dal.Repositories
{
    public class NoteRepository : INoteRepository
    {
        //private readonly List<NoteDto> _notes = new List<NoteDto>();
        private int _nextId = 1;
        private NotesDbContext _dbContext;

        public NoteRepository(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            var note = await _dbContext.Notes.Include(s => s.Shares).FirstOrDefaultAsync(n => n.Id == id);
            return note;
        }

        public async Task<IEnumerable<Note>> GetAllNotesByUserIdAsync(Guid userId)
        {
            return await _dbContext.Notes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task AddNoteAsync(Note note)
        {
            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            if (!_dbContext.Notes.Any(x => x.Id == note.Id))
            {
                throw new Exception("Заметка не найдена");
            }
            try
            {
                _dbContext.Entry(note).State = EntityState.Modified;
                //_dbContext.Notes.Update(note);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task DeleteNoteAsync(int id)
        {
            var noteToRemove = await _dbContext.Notes.FindAsync(id);
            if (noteToRemove != null)
            {
                _dbContext.Notes.Remove(noteToRemove);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetSharedNotesByUserIdAsync(Guid userId)
        {
            return await _dbContext.Notes
                .Where(x => (x.IsPublic || x.Shares != null && x.Shares.Any(y => y.UserId == userId)) && x.UserId != userId)
                .ToListAsync();
        }

        public async Task UpdateNoteSharesAsync(int noteId, List<ShareItemDto> noteShareDtos)
        {
            // Удаляем существующие записи о shared users для этой заметки
            _dbContext.NoteShares.RemoveRange(_dbContext.NoteShares.Where(x => x.NoteId == noteId));
            foreach (var item in noteShareDtos)
            {
                var sharedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == item.UserId);
                if (sharedUser != null)
                {
                    var n = new NoteShare();

                    _dbContext.NoteShares.Add(new NoteShare
                    {
                        NoteId = noteId,
                        UserId = item.UserId,
                        CanEdit = item.CanEdit
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> NoteExistAsync(int id)
        {
            return await _dbContext.Notes.AnyAsync(e => e.Id == id);
        }
    }
}
