using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TweetBook.Domain;
using TweetBook.Options;

namespace TweetBook.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
        public async Task<AuthenticationResult> RegisterAsync(string Email, string Password)
        {
            var existingUser = await _userManager.FindByEmailAsync(Email);

            if (existingUser != null)
                return new AuthenticationResult { Errors = new[] { "User with this email already exists" } };

            var newUser = new IdentityUser
            {
                Email = Email,
                UserName = Email
            };

            var createduser = await _userManager.CreateAsync(newUser, Password);

            if (!createduser.Succeeded)
                return new AuthenticationResult { Errors = createduser.Errors.Select(x => x.Description) };

            return GenerateAuthenticationResultForUser(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            
            if (user == null)
                return new AuthenticationResult { Errors = new[] { "User does not exist" } };
            
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
            
            if (!userHasValidPassword)
                return new AuthenticationResult { Errors = new[] { "User does not have a valid password" } };
            
            return GenerateAuthenticationResultForUser(user);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new [] {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("id", user.Id),
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult{
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}