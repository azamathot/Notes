using Notes.Common.DTOs;

namespace Notes.Bll.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task AddUserAsync(UserDto user);
        Task<bool> ExistUserAsync(Guid id);
    }
}
