using Vehicle.InsurancePolicies.Domain.Context;

namespace Vehicle.InsurancePolicies.API.Installers
{
  class MongoInstaller : IInstaller
  {
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
      string connectionString = configuration.GetConnectionString(CommonValues.VehicleInsurancePoliciesConnection);
      services.AddMongoDbContext<VehicleInsurancePoliciesContext>(options => options.ConnectionString = connectionString);
    }
  }
}
