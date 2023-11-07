using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ECommerce.Application.Abstraction;
using ECommerce.Application.Helpers;
using ECommerce.Application.ViewModels.Token;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Services;

public class TokenHandlerService : ITokenHandlerService
{
    readonly IConfiguration _configuration;

    public TokenHandlerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenViewModel CreateAccessToken(Customer customer)
    {
        TokenViewModel token = new();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        
        token.Expiration = DateTime.UtcNow.AddMinutes(CodebaseHelpers.TokenExpireTime);
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: new List<Claim>
            {
                new(ClaimTypes.Name, customer.UserName),
                new("UserId", customer.Id.ToString())
            }
        );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);

        token.RefreshToken = CreateRefreshToken();
        return token;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}