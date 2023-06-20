using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vehicle.InsurancePolicies.API.Options;
using Vehicle.InsurancePolicies.Contracts.DTO.Auth;
using Vehicle.InsurancePolicies.Contracts.Identity;
using Vehicle.InsurancePolicies.Contracts.Exceptions;

namespace Vehicle.InsurancePolicies.API.Services
{
  class IdentityService : IIdentityService
  {
    readonly JwtOptions _jwtOptions;

    public IdentityService(IOptions<JwtOptions> options) => _jwtOptions = options.Value;

    public AuthResponse Authenticate()
    {
      try
      {
        (byte[] key, _) = _jwtOptions.GetGeneratedKey();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
          Subject = new(new[]
          {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString()),
          }),
          Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpiresInDays ?? 0),
          SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        return new()
        {
          Token = token
        };
      }
      catch (Exception exception)
      {
        throw new ServiceErrorException(
          HttpStatusCode.Unauthorized,
          exception.Message,
          exception.InnerException?.Message!);
      }
    }
  }
}
