using Microsoft.EntityFrameworkCore;
using Notes.Dal.Entities;

namespace Notes.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private NotesDbContext _dbContext;
        public UserRepository(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistUserAsync(Guid id)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == id && !u.IsDelete);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
