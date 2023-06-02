using System.Reflection;
using Vehicle.InsurancePolicies.API.Installers;

namespace Vehicle.InsurancePolicies.API.Extensions
{
  static class InstallerExtensions
  {
    public static void InstallServicesFromAssembly(
      this IServiceCollection services,
      IConfiguration configuration,
      IWebHostEnvironment env)
    {
      var installers = Assembly.GetExecutingAssembly().GetTypes()
          .Where(type => typeof(IInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
          .Select(Activator.CreateInstance)
          .Cast<IInstaller>()
          .ToArray();
      foreach (IInstaller installer in installers)
        installer.InstallServices(services, configuration, env);
    }
  }
}
