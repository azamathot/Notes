using AutoMapper;
using Notes.Bll.Interfaces;
using Notes.Common.DTOs;
using Notes.Dal.Entities;
using Notes.Dal.Repositories;

namespace Notes.Bll.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<NoteDto?> GetNoteByIdAsync(int id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return note != null ? _mapper.Map<NoteDto>(note) : null;
        }

        public async Task<NoteDto2> AddNoteAsync(NoteDto2 noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            await _noteRepository.AddNoteAsync(note);
            noteDto = _mapper.Map<NoteDto2>(note);
            return noteDto;
        }

        public async Task<ResponceDto<NoteDto2>> UpdateNoteAsync(NoteDto2 noteDto, Guid userId)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteDto.Id.Value);
            if (!(note?.UserId == userId || (note?.Shares?.Any(x => x.UserId == userId && x.CanEdit) ?? false)))
            {
                return new ResponceDto<NoteDto2>
                {
                    Status = StatusCode.Forbid
                };
            }
            note.Title = noteDto.Title;
            note.Body = noteDto.Body;
            note.Color = noteDto.Color;
            note.IsPublic = noteDto.IsPublic;
            await _noteRepository.UpdateNoteAsync(note);
            return new ResponceDto<NoteDto2>
            {
                Status = StatusCode.Success,
                Value = _mapper.Map<NoteDto2>(note)
            };
        }

        public async Task DeleteNoteAsync(int id)
        {
            await _noteRepository.DeleteNoteAsync(id);
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesByUserIdAsync(Guid userId)
        {
            var notes = await _noteRepository.GetAllNotesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task<IEnumerable<NoteDto>> GetSharedNotesByUserIdAsync(Guid userId)
        {
            var notes = await _noteRepository.GetSharedNotesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task UpdateNoteSharesAsync(int noteId, List<ShareItemDto> noteShares)
        {
            await _noteRepository.UpdateNoteSharesAsync(noteId, noteShares);
        }

        public async Task<bool> NoteExistAsync(int id)
        {
            return await _noteRepository.NoteExistAsync(id);
        }
    }
}
