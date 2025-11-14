using Microsoft.EntityFrameworkCore;
using Playground.Domain.Entities;
using Playground.Persistance;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(PlaygroundDbContext context)
        : base(context)
    {
    }

    public async Task<User?> GetUserWithPostsAsync(int userId)
    {
        return await _dbSet
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}