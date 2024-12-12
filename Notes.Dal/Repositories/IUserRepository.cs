using Notes.Dal.Entities;

namespace Notes.Dal.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task<bool> ExistUserAsync(Guid id);
    }
}
