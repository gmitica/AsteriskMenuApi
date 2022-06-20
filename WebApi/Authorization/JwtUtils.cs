using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data.Access;
using WebApi.Helpers;

namespace WebApi.Authorization
{

    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }

    public class JwtUtils : IJwtUtils
    {
        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public JwtUtils(
            ApplicationDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 2 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim("UserFirstName", user.FirstName));
            claims.Add(new Claim("UserLastName", user.LastName));
            claims.Add(new Claim("UserEmail", user.Email));
            claims.Add(new Claim("IsManaged", user.IsManaged.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenMinutesExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Guid? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                Expires = DateTime.UtcNow.AddDays(14),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            return refreshToken;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                var tokenIsUnique = !_context.Users.Any(u => u.RefreshTokens.Any(t => t.Token == token));

                if (!tokenIsUnique)
                    return getUniqueToken();

                return token;
            }
        }
    }
}