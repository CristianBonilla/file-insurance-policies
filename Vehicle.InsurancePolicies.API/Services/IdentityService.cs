using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vehicle.InsurancePolicies.API.Options;
using Vehicle.InsurancePolicies.Contracts.Identity;
using Vehicle.InsurancePolicies.Contracts.Identity.Auth;

namespace Vehicle.InsurancePolicies.API.Services
{
  class IdentityService : IIdentityService
  {
    readonly JwtOptions _jwtOptions;

    public IdentityService(IOptions<JwtOptions> options) => (_jwtOptions) = options.Value;

    public AuthResponse Authenticate()
    {
      byte[] key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
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
  }
}
