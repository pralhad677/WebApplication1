using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Util
{
    public class TokenGeneration
    {
        public string GenerateToken(string issuer, string audience, string email)
        {
            // Generate a secret key of sufficient length
            var secretKey = GenerateSecretKey();

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email), // Use ClaimTypes.Email for email address
                // Add other relevant claims if needed (e.g., roles)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // Set an appropriate expiration time
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateSecretKey()
        {
            var bytes = new byte[32]; // 256 bits
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }
    }
}
