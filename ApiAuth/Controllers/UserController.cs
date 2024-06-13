using ApiAuth.Dtos;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarUsuario([FromBody] CreateUserDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Password != request.ConfirmPassword)
                return BadRequest(new { mensagem = "Senha e confirmação de senha não coincidem" });

            try
            {
                var user = await _userService.CriarUsuario(request.Name, request.Email, request.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AuthUserDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var auth = await _userService.AutenticarUsuario(request.Email, request.Password);
                return Ok(auth);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            return $"Autenticado - Nome: {userName}, Email: {userEmail}, ID: {userId}";
        }
    }
}
