using Historias.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Historias.API
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var token = await userRepository.Login(email, password);

            if (token == null) return BadRequest("Usuário ou senha não batem.");

            return Ok(token);
        }
    }
}