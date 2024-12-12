using AutoMapper;
using Notes.Common.DTOs;
using Notes.Dal.Entities;

namespace Notes.Bll.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Note, NoteDto2>().ReverseMap();
            CreateMap<NoteDto, NoteDto2>().ReverseMap();
            CreateMap<NoteShare, NoteShareDto>().ReverseMap();
        }
    }
}
