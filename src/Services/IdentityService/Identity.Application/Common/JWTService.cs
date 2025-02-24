using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Application.Common
{
    public static class JWTService
    {
        /// <summary>
        /// Get token.
        /// </summary>
        /// <param name="authClaims">Auth claims</param>
        /// <param name="jwtSecret">Jwt secret</param>
        /// <param name="jwtValidIssuer">Jwt valid issuer</param>
        /// <param name="jwtValidAudience">Jwt valid audience</param>
        /// <param name="jwtExpiryDays">Jwt expiry days</param>
        /// <returns>JwtSecurityToken</returns>
        public static JwtSecurityToken GetJWTToken(List<Claim> authClaims, string jwtSecret, string jwtValidIssuer, string jwtValidAudience, string jwtExpiryDays)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                issuer: jwtValidIssuer,
                audience: jwtValidAudience,
                expires: DateTime.Now.AddDays(String.IsNullOrWhiteSpace(jwtExpiryDays) ? 7 : Convert.ToInt64(jwtExpiryDays)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }


        /// <summary>
        /// Get Token Claims
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userId">User identity</param>
        /// <param name="name">Name</param>
        /// <param name="role">Role</param>
        /// <returns>Claims</returns>
        public static List<Claim> GetTokenClaims(string email, string userId, string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.GivenName, name ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, role)
            };
            return claims;
        }
    }
}
