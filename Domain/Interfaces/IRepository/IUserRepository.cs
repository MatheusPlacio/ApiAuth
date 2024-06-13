using ApiAuth.Interfaces;
using ApiAuth.models;

namespace ApiAuth.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
