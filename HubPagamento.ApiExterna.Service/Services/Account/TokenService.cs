using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Services.Account
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _settings;
        public TokenService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<AuthorizeResponse> GenerateToken(string login, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login),
                    //new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _settings.JwtSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenWrite = tokenHandler.WriteToken(token);

            var response = new AuthorizeResponse(tokenWrite, "token gerado com sucesso");

            return response;
        }
    }
}
