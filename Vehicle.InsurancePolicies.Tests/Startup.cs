using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Vehicle.InsurancePolicies.Domain.Context;

namespace Vehicle.InsurancePolicies.Tests
{
  class Startup
  {
    private static Startup _startup = null!;

    public IServiceCollection ServiceCollection { get; private set; } = null!;

    public static Startup Instance
    {
      get
      {
        _startup ??= Create();

        return _startup;
      }
    }

    private Startup() { }

    private static Startup Create()
    {
      Startup startup = new();
      IServiceCollection serviceCollection = new ServiceCollection();
      IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .Build();
      Mock<VehicleInsurancePoliciesContext> mockContext = new();
      serviceCollection.AddSingleton(configuration);
      serviceCollection.AddScoped(_ => mockContext.Object);
      startup.ServiceCollection = serviceCollection;

      return startup;
    }
  }
}
