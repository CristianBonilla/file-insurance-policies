using Microsoft.OpenApi.Models;
using Vehicle.InsurancePolicies.API.Options;

namespace Vehicle.InsurancePolicies.API.Installers
{
  public class SwaggerInstaller : IInstaller
  {
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
      IConfigurationSection swaggerSection = configuration.GetSection(nameof(SwaggerOptions));
      services.Configure<SwaggerOptions>(swaggerSection);
      SwaggerOptions swagger = swaggerSection.Get<SwaggerOptions>();
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new()
        {
          Title = "Vehicle Insurance Policies API",
          Version = "v1",
          Description = "Create an API to process vehicle insurance policies and that allows searches",
          Contact = swagger.Contact
        });
        options.AddSecurityDefinition(CommonValues.Bearer, new()
        {
          Description = "JWT Auth Token header using the bearer schema",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });
        OpenApiSecurityScheme apiSecurity = new()
        {
          Reference = new OpenApiReference
          {
            Id = CommonValues.Bearer,
            Type = ReferenceType.SecurityScheme
          }
        };
        options.AddSecurityRequirement(new OpenApiSecurityRequirement { { apiSecurity, new List<string>() } });
      });
    }
  }
}
