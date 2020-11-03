using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCore_JwtAuthenticate.Models;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore_JwtAuthenticate.Services
{
    public class AccessTokenService
    {
        private readonly AccessTokenConfiguration _accessTokenConfigurations;

        public AccessTokenService(AccessTokenConfiguration accessTokenConfigurations)
        {
            _accessTokenConfigurations = accessTokenConfigurations;
        }

        public AccessToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_accessTokenConfigurations.Secret);

            AccessToken accessToken = new AccessToken();

            accessToken.CreateIn = DateTime.Now;
            accessToken.ExpiresIn = accessToken.CreateIn + TimeSpan.FromHours(_accessTokenConfigurations.Hours);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]    //Identificação do usuário
                {
                    new Claim(ClaimTypes.Email, user.email.ToString()),
                }),
                Expires = accessToken.ExpiresIn,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _accessTokenConfigurations.Issuer, //Nome da API que gerou o token
                Audience = _accessTokenConfigurations.Audience,  //Quem utilizará o token.
                NotBefore = accessToken.CreateIn //Identifica que o token não é válido antes da hora definida
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            accessToken.Token = tokenHandler.WriteToken(token);

            return accessToken;

        }
    }
}