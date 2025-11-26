using Playground.Domain.Entities;

namespace Playground.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<User>> GetTopUsersAsync(int userId);
}