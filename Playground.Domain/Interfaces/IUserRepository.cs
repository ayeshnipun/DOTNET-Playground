using Playground.Domain.Entities;
using Playground.Domain.Interfaces;

namespace Playground.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserWithPostsAsync(int userId);
}