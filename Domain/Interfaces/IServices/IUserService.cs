using ApiAuth.models;
using Domain.JWT;

namespace Domain.Interfaces.IServices
{
    public interface IUserService
    {
        Task<User> CriarUsuario(string name, string email, string password);
        Task<AuthResponse> AutenticarUsuario(string email, string password);
    }
}
