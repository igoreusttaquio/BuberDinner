using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Services;
using BuberDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;
    public string GenerateToken(User user)
    {
        //var _key = new byte[32];
        //using (var rng = RandomNumberGenerator.Create())
        //{
        //    rng.GetBytes(_key);
        //}

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            //new SymmetricSecurityKey(_key),
            SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };


        var securityToken = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, issuer: _jwtSettings.Issuer, expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes), audience: _jwtSettings.Audience);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

}
