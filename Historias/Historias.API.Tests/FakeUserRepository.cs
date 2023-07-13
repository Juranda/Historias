using Historias.API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Historias.API.Tests
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<(string Email,  string Password)> users;

        public FakeUserRepository()
        {
            users = new List<(string, string)>
            {
                new ("emailfake@gmail.com", "12341234"),
                new ("emailmaisfake@gmail.com", "43214321"),
                new ("hackermestre@yahoo.com", "34145234")
            };
        }

        public Task<string?> Login(string email, string password)
        {
            (string Email, string Password) user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == default) return Task.FromResult<string>(null);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(claims: claims);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}