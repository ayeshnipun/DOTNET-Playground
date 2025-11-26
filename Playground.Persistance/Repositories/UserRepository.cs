using Microsoft.EntityFrameworkCore;
using Playground.Domain.Entities;
using Playground.Domain.Interfaces;
using Playground.Persistance;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(PlaygroundDbContext context)
        : base(context)
    {
    }

    public Task<List<User>> GetTopUsersAsync(int userId)
    {
        // var filterDate = DateTime.UtcNow.AddMonths(-1);
        // var users = _dbSet
        //     .Include(u => u.Posts
        //         .Where(p => p.CategoryId == 1))
        //     .ThenInclude(p => p.Likes)
        //     .OrderByDescending(u => u.Posts).Take(5).ToList();
        var users = _dbSet
            .Include(u => u.Posts)
                .ThenInclude(p => p.Likes)
            .Include(u => u.Comments)
            .OrderByDescending(u => u.Comments.Count)
            .Take(userId)
            .ToList();
        return Task.FromResult(users);
    }
}