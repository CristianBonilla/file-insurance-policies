using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Vehicle.InsurancePolicies.API.Options;

namespace Vehicle.InsurancePolicies.API.Installers
{
  class JwtInstaller : IInstaller
  {
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
      IConfigurationSection jwtSection = configuration.GetSection(nameof(JwtOptions));
      services.Configure<JwtOptions>(jwtSection);
      JwtOptions jwtOptions = jwtSection.Get<JwtOptions>();
      services.AddSingleton(jwtOptions);
      byte[] key = Encoding.ASCII.GetBytes(jwtOptions.Secret);
      services.AddAuthentication(auth =>
      {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(jwt =>
      {
        jwt.RequireHttpsMetadata = !env.IsDevelopment(); // Development only
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          RequireExpirationTime = false,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        };
      });
      services.AddAuthorization();
    }
  }
}
