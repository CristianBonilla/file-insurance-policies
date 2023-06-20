using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
      serviceCollection.AddSingleton(configuration);
      startup.ServiceCollection = serviceCollection;

      return startup;
    }
  }
}
