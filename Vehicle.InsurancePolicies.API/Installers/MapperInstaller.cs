using AutoMapper;
using Vehicle.InsurancePolicies.API.Mappings;

namespace Vehicle.InsurancePolicies.API.Installers
{
  class MapperInstaller : IInstaller
  {
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
      MapperConfiguration mapperConfiguration = MapperStart.Build();
      IMapper mapper = mapperConfiguration.CreateMapper();
      services.AddSingleton(mapper);
    }
  }
}
