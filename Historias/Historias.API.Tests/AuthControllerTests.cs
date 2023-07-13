using FluentAssertions;
using Historias.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Historias.API.Tests
{
    public class AuthControllerTests
    {
        [Theory]
        [InlineData("emailfake@gmail.com", "12341234")]
        [InlineData("emailmaisfake@gmail.com", "43214321")]
        [InlineData("hackermestre@yahoo.com", "34145234")]
        public async void Login_UserExists_ReturnsToken(string email, string password)
        {
            IUserRepository userRepository = new FakeUserRepository();
            AuthController controller = new AuthController(userRepository);

            var result = await controller.Login(email, password);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            okObjectResult.Value.Should().NotBeNull();
            string token = okObjectResult.Value as string;

            JwtSecurityToken jwt = new JwtSecurityToken(token);

            bool valid = jwt.Claims
                .Where(c => c.Type == ClaimTypes.Email && c.Value == email)
                .Any();

            jwt.Claims.Should().HaveCount(1);
            
            valid.Should().Be(true);
        }
    }
}