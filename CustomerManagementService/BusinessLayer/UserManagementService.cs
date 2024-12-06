using CustomerManagementService.Models;
using CustomerManagementService.Models.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomerManagementService.BusinessLayer
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptionsMonitor<JWTConfiguration> _jwtConfiguration;

        public UserManagementService(UserManager<IdentityUser> userManager, 
                                     IOptionsMonitor<JWTConfiguration> jwtConfiguration)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration;
        }

        public async Task<string> LoginUser(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return string.Empty;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.CurrentValue.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.CurrentValue.ValidIssuer,
                audience: _jwtConfiguration.CurrentValue.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
