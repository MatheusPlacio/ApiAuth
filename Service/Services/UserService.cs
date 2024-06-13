using ApiAuth.models;
using ApiAuth.Repository;
using Domain.Interfaces.IServices;
using Domain.JWT;

namespace ApiAuth.services.user
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CriarUsuario(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Todos os campos são obrigatórios!");

            var usuarioJaExiste = await _userRepository.GetByEmail(email);
            if (usuarioJaExiste != null)
                throw new Exception("Email já cadastrado!");

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            User novoUsuario = new User
            {
                Name = nome,
                Email = email,
                Password = senhaHash
            };

            await _userRepository.Add(novoUsuario);
            return novoUsuario;
        }

        public async Task<AuthResponse> AutenticarUsuario(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Email e senha são obrigatórios!");

            User usuario = await _userRepository.GetByEmail(email);
            if (usuario == null)
                throw new Exception("Email ou senha incorretos!");

            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(senha, usuario.Password);
            if (!senhaCorreta)
                throw new Exception("Senha incorreta!");

            var token = TokenService.GenerateToken(usuario);

            AuthResponse authResponse = new AuthResponse()
            {
                Id = usuario.Id,
                Name = usuario.Name,
                Email = usuario.Email,
                Token = token
            };

            return authResponse;
        }

    }
}
