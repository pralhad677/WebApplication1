using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Util
{
    public class TokenValidation
    {
        private string secretKey = "your_new_secret_key_with_at_least_16_characters";

        public ClaimsPrincipal ValidateTokenAndGetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "your_issuer",
                ValidAudience = "your_audience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            try
            {
                // Validate token and get claims
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                // Token validation failed
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string ExtractUserEmailFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
