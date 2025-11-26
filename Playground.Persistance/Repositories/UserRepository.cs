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
        return Task.FromResult(new List<User>());
    }
}